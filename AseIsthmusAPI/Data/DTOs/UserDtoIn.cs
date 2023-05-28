using System.ComponentModel.DataAnnotations;

namespace AseIsthmusAPI.Data.DTOs
{
    public class UserDtoIn
    {
        [MaxLength(12, ErrorMessage ="El código de empleado debe ser menor a 12 caracteres.")]
        public string PersonId { get; set; }

        [MaxLength(12, ErrorMessage = "El número de identificación debe ser menor a 12 caracteres.")]
        public string NumberId { get; set; }

        [MaxLength(20, ErrorMessage = "El nombre empleado debe ser menor a 20 caracteres.")]
        public string FirstName { get; set; }

        [MaxLength(20, ErrorMessage = "El primer apellido debe ser menor a 20 caracteres.")]
        public string LastName1 { get; set; }

        [MaxLength(20, ErrorMessage = "El segundo apellido debe ser menor a 20 caracteres.")]
        public string? LastName2 { get; set; }

        [MaxLength(3, ErrorMessage = "El código de país debe ser menor a 3 caracteres.")]
        public string Nationality { get; set; }

        public DateTime DateBirth { get; set; }

        public DateTime WorkStartDate { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        [MaxLength(20, ErrorMessage = "El número de teléfono debe ser menor a 20 caracteres.")]
        public string PhoneNumber { get; set; }

        [MaxLength(50, ErrorMessage = "El correo electrónico debe ser menor a 50 caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del correo es incorrecto.")]
        public string EmailAddress { get; set; }

        [MaxLength(25, ErrorMessage = "La cuenta bancaria debe ser menor a 25 caracteres.")]
        public string BankAccount { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        [MaxLength(150, ErrorMessage = "La dirección 2 debe ser menor a 150 caracteres.")]
        public string Address1 { get; set; }

        [MaxLength(150, ErrorMessage = "La dirección 2 debe ser menor a 150 caracteres.")]
        public string? Address2 { get; set; }

        public int DistrictId { get; set; }

        [MaxLength(10, ErrorMessage = "El código postal debe ser menor a 10 caracteres.")]
        public string PostalCode { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
