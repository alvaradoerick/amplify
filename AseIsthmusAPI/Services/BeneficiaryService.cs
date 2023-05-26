using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Data;

namespace AseIsthmusAPI.Services
{
    public class BeneficiaryService
    {
        private readonly AseIsthmusContext _context;
        public BeneficiaryService(AseIsthmusContext context)
        {
            _context = context;
        }

        public async Task<Beneficiary> Create(string id, BeneficiaryDtoIn beneficiary)
        {
            var newBeneficiary = new Beneficiary
            {
                PersonId = id,
                 BeneficiaryName = beneficiary.BeneficiaryName,
                 BeneficiaryNumberId = beneficiary.BeneficiaryNumberId,
                 BeneficiaryRelation = beneficiary.BeneficiaryRelation,
                 BeneficiaryPercentage = beneficiary.BeneficiaryPercentage,         
            };
            _context.Beneficiaries.Add(newBeneficiary);

            await _context.SaveChangesAsync();
            return newBeneficiary;

        }
    }
}
