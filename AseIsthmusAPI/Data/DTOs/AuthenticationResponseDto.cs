using System.ComponentModel.DataAnnotations;

namespace AseIsthmusAPI.Data.DTOs
{
    public class AuthenticationResponseDto
    {
        public string? Token { get; set; }
        public string? PersonId { get; set; }
        public int RoleId { get; set; }
    }
}
