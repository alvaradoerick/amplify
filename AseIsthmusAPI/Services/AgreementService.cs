using Microsoft.EntityFrameworkCore;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using AseIsthmusAPI.Data.DTOs;

namespace AseIsthmusAPI.Services
{
    public class AgreementService
    {

        private readonly AseItshmusContext _context;

        public AgreementService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<Agreement> Create(AgreementDtoIn newAgreementDto)
        {
           
            
                var agreement = new Agreement
                {
                    Title = newAgreementDto.Title,
                    Description = newAgreementDto.Description,
                   // Image = newAgreementDto.Image,
                    CategoryAgreementId = newAgreementDto.CategoryAgreementId,
                    IsActive = newAgreementDto.IsActive
                };

                _context.Agreements.Add(agreement);
                await _context.SaveChangesAsync();

                return agreement;
        }
    }
}
