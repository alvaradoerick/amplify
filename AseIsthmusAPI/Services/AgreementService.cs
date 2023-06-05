using Microsoft.EntityFrameworkCore;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AseIsthmusAPI.Services
{
    public class AgreementService
    {

        private readonly AseItshmusContext _context;

        public AgreementService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<Agreement> Create([FromForm]Agreement newAgreement)
        {
           
            
                var agreement = new Agreement
                {
                    AgreementId = newAgreement.AgreementId,
                    Title = newAgreement.Title,
                    Description = newAgreement.Description,
                    Image = await ReadAllBytesAsync(newAgreement.Image.OpenReadStream()),
                    CategoryAgreementId = newAgreement.CategoryAgreementId,
                    IsActive = newAgreement.IsActive,
                    PersonId = newAgreement.PersonId

                };

                _context.Agreements.Add(agreement);
                await _context.SaveChangesAsync();

                return agreement;
        }
    }
}
