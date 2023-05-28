using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;

namespace AseIsthmusAPI.Services
{
    public class BeneficiaryService
    {
        private readonly AseItshmusContext _context;
        public BeneficiaryService(AseItshmusContext context)
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
