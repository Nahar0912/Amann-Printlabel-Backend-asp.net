namespace Backend.DTOs
{
    public class JwtPayload
    {
        public int Sub { get; set; } // User ID
        public required string Email { get; set; }
    }
}
