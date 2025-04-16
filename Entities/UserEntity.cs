using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Entities
{
    [Table("UserInfo", Schema = "dbo")]
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string? Username { get; set; }

        [MaxLength(200)]
        public required string Email { get; set; }

        [Required,JsonIgnore, MaxLength(200)]
        public string PasswordHash { get; set; }

        [Required, MaxLength(200)]
        public string? Role { get; set; }
        [JsonIgnore]
        public DateTime Created { get; set; } = DateTime.Now;

        [JsonIgnore]
        public DateTime Modified { get; set; } = DateTime.Now;

        public bool? isActive { get; set; }

    }
}
