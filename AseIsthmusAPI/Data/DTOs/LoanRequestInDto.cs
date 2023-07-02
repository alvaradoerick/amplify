namespace AseIsthmusAPI.Data.DTOs
{
    public class LoanRequestInDto
    {
        public int LoansTypeId { get; set; }

        public decimal AmountRequested { get; set; }

        public int Term { get; set; }

        public string BankAccount { get; set; } = null!;


    }
}
