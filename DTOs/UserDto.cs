using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class UserDto
    {
        public string? Username { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required, MinLength(6)]
        public required string PasswordHash { get; set; }

        public string? Role { get; set; }

        public bool? isActive { get; set; } = true;
    }

    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public bool? isActive { get; set; }
    }
}
