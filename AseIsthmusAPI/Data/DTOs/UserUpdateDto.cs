using System.ComponentModel.DataAnnotations;

namespace AseIsthmusAPI.Data.DTOs
{
    public class UserUpdateDto
    {
        [MaxLength(20, ErrorMessage = "El número de teléfono debe ser menor a 20 caracteres.")]
        public string PhoneNumber { get; set; }

        [MaxLength(25, ErrorMessage = "La cuenta bancaria debe ser menor a 25 caracteres.")]
        public string BankAccount { get; set; }

        [MaxLength(150, ErrorMessage = "La dirección 2 debe ser menor a 150 caracteres.")]
        public string Address1 { get; set; }

        [MaxLength(150, ErrorMessage = "La dirección 2 debe ser menor a 150 caracteres.")]
        public string? Address2 { get; set; }

        public int DistrictId { get; set; }

        [MaxLength(10, ErrorMessage = "El código postal debe ser menor a 10 caracteres.")]
        public string PostalCode { get; set; }

    }
}
