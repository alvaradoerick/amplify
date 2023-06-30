namespace AseIsthmusAPI.Data.DTOs
{
    public class SavingsTypeInDto
    {
        public string? Description { get; set; }

        public DateTime? ApplicationDeadline { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
