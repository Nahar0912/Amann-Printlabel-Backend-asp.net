namespace Backend.DTOs
{
    public class JwtPayload
    {
        public int Sub { get; set; } // User ID
        public string Email { get; set; }
    }
}
