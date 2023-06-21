using AseIsthmusAPI.Data.AseIsthmusModels;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data.DTOs
{
    public class AgreementDtoOut
    {
        
        public int AgreementId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Image { get; set; }
        public int CategoryAgreementId { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool IsActive { get; set; }

    }
}
