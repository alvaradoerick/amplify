namespace AseIsthmusAPI.Data.DTOs
{
    public class LoanRequestOutDto
    {
        public int LoanRequestId { get; set; }

        public int LoansTypeId { get; set; }

        public decimal AmountRequested { get; set; }
        public string PersonId { get; set; }
        public string Name { get; set; }
        public string NumberId { get; set; }
        public string LoanTypeName { get; set; }

        public int Term { get; set; }

        public DateTime RequestedDate { get; set; }

        public string BankAccount { get; set; } = null!;

        public bool IsActive { get; set; }

        public bool? IsApproved { get; set; }

        public DateTime? ApprovedDate { get; set; }

    }
}
