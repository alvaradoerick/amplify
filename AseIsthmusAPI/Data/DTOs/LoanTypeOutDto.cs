namespace AseIsthmusAPI.Data.DTOs
{
    public class LoanTypeOutDto
    {
        public int LoansTypeId { get; set; }

        public string LoanDescription { get; set; } = null!;

        public int ContributionUsageId { get; set; }

        public decimal PercentageEmployeeCont { get; set; }

        public decimal? PercentageEmployerCont { get; set; }

        public int Term { get; set; }

        public decimal InterestRate { get; set; }

        public bool IsActive { get; set; }

        public string ContributionUsageDescription { get; set; }
    }
}
