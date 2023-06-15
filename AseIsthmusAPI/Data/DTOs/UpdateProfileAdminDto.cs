using System.ComponentModel.DataAnnotations;

namespace AseIsthmusAPI.Data.DTOs
{
    public class UpdateProfileAdminDto
    {
        [MaxLength(12, ErrorMessage = "El código de empleado debe ser menor a 12 caracteres.")]
        public string PersonId { get; set; }

        [MaxLength(12, ErrorMessage = "El número de identificación debe ser menor a 12 caracteres.")]
        public string NumberId { get; set; }

        [MaxLength(20, ErrorMessage = "El nombre empleado debe ser menor a 20 caracteres.")]
        public string FirstName { get; set; }

        [MaxLength(20, ErrorMessage = "El primer apellido debe ser menor a 20 caracteres.")]
        public string LastName1 { get; set; }

        [MaxLength(20, ErrorMessage = "El segundo apellido debe ser menor a 20 caracteres.")]
        public string? LastName2 { get; set; }

        public DateTime DateBirth { get; set; }

        public DateTime WorkStartDate { get; set; }

        public DateTime? EnrollmentDate { get; set; }
      
        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        public DateTime? ApprovedDate { get; set; }
    }
}
