using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("UserInfo", Schema = "dbo")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Username { get; set; }

        [Required, MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(200)]
        public string PasswordHash { get; set; }

        [MaxLength(200)]
        public string Role { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public bool IsActive { get; set; }
    }
}
