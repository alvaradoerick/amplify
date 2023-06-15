namespace AseIsthmusAPI.Data.DTOs
{
    public class UserDtoOut
    {
        public string PersonId { get; set; } = null!;

        public string NumberId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName1 { get; set; } = null!;

        public string? LastName2 { get; set; }

        public string Nationality { get; set; } = null!;

        public DateTime DateBirth { get; set; }

        public DateTime WorkStartDate { get; set; }

        public DateTime? EnrollmentDate { get; set; } 

        public string PhoneNumber { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string BankAccount { get; set; } = null!;

        public bool IsActive { get; set; }

        public int RoleId { get; set; }

        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        public int DistrictId { get; set; }

        public string PostalCode { get; set; } = null!;

        public DateTime? ApprovedDate { get; set; }

        public string? RoleDescription { get; set; }
        
    }
}
