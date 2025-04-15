using Backend.DTOs;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Backend.Data;

namespace Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Health check method
        public string GetIndex()
        {
            return "User Service is running";
        }

        // Get all users
        public async Task<IQueryable<UserEntity>> FindAllAsync()
        {
            return _context.Users.AsQueryable();
        }

        // Get a user by ID
        public async Task<UserEntity> FindOneByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found");
            }
            return user;
        }

        // Find a user by email (used for login)
        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }

        // Validate password by comparing plain text password with hashed password
        public bool ValidatePassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        // Create a new user
        public async Task<UserEntity> CreateAsync(UserDto userDto)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash, salt);

            var user = new UserEntity
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = hashedPassword,
                Role = userDto.Role,
                isActive = userDto.isActive ?? true,  // Default to true if not provided
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Update a user by ID
        public async Task<UserEntity> UpdateAsync(int id, UpdateUserDto userDto)
        {
            var user = await FindOneByIdAsync(id);

            user.Username = userDto.Username ?? user.Username;
            user.Email = userDto.Email ?? user.Email;
            user.Role = userDto.Role ?? user.Role;
            user.isActive = userDto.isActive ?? user.isActive;
            user.Modified = DateTime.Now;

            // If password is provided, hash it and update
            if (!string.IsNullOrEmpty(userDto.PasswordHash))
            {
                var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash, salt);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // Delete a user by ID
        public async Task DeleteAsync(int id)
        {
            var user = await FindOneByIdAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    // Custom exception class for "Not Found"
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
