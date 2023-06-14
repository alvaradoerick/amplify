namespace AseIsthmusAPI.Data.DTOs
{
    public class UserDtoOut
    {
        public string PersonId { get; set; } 

        public string NumberId { get; set; } 

        public string FirstName { get; set; } 

        public string LastName1 { get; set; } 

        public string? LastName2 { get; set; }

        public string Nationality { get; set; } 

        public DateTime DateBirth { get; set; }

        public DateTime WorkStartDate { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public string PhoneNumber { get; set; } 

        public string EmailAddress { get; set; } 

        public string BankAccount { get; set; } 

        public bool IsActive { get; set; }

        public string RoleDescription { get; set; }

        public string Address1 { get; set; } 

        public string? Address2 { get; set; }

        public int DistrictId { get; set; }

        public string PostalCode { get; set; } 
        public DateTime? ApprovedDate { get; set; }
    }
}
