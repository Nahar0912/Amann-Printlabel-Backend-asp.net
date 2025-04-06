using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserEntity?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> ValidatePassword(string plainPassword, string hashedPassword)
        {
            return hashedPassword == HashPassword(plainPassword);
        }

        private string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public async Task<UserEntity> CreateUser(UserDto userDto)
        {
            var user = new UserEntity
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.PasswordHash),
                Role = userDto.Role,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                IsActive = userDto.IsActive
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity?> UpdateUser(int id, UserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Username = userDto.Username ?? user.Username;
            user.Email = userDto.Email ?? user.Email;
            if (!string.IsNullOrEmpty(userDto.PasswordHash))
            {
                user.PasswordHash = HashPassword(userDto.PasswordHash);
            }
            user.Role = userDto.Role ?? user.Role;
            user.IsActive = userDto.IsActive;
            user.Modified = DateTime.Now;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
