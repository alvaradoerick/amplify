namespace AseIsthmusAPI.Data.DTOs
{
    public class AgreementDtoIn
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Image { get; set; } = null!;
        public int CategoryAgreementId { get; set; }
        public bool IsActive { get; set; }

    }
}
