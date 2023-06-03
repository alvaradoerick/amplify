namespace AseIsthmusAPI.Data.DTOs
{
    public class AuthenticationResponseDto
    {
        public string? Token { get; set; }
        public User? User { get; set; }
    }
}
