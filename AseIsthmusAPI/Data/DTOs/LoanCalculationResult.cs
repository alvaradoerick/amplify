namespace AseIsthmusAPI.Data.DTOs
{
    public class LoanCalculationResult
    {
        public decimal AvailEmployeeAmt { get; set; }
        public decimal AvailEmployerAmt { get; set; }
        public decimal TotalAvailAmount { get; set; }
        public decimal BiweeklyFee { get; set; }
        public decimal TotalAmtToPay { get; set; }
    }
}
