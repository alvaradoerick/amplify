namespace AseIsthmusAPI.Models
{
    public class Person
    {
        public string PersonId { get; set; }
        public string NumberId { get; set; }
        public string FirstName { get; set; }
        public string LastName1 { get; set; }
        public string? LastName2 { get; set; }
        public string Nationality { get; set; }
        public DateOnly DateBirth { get; set; }
        public DateOnly WorkStartDate { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BankAccount { get; set; }
        public bool IsActive { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public int Province { get; set; }
        public int Canton { get; set; }
        public int District { get; set; }
        public string PostalCode { get; set; }

    }

}