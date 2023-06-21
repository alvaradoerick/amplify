using Microsoft.EntityFrameworkCore;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using AseIsthmusAPI.Data.DTOs;
using System.Collections;

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
                    Image = newAgreementDto.Image,
                    CategoryAgreementId = newAgreementDto.CategoryAgreementId,
                    IsActive = newAgreementDto.IsActive
                };

                _context.Agreements.Add(agreement);
                await _context.SaveChangesAsync();

                return agreement;
        }

        public async Task<IEnumerable<AgreementDtoOut>> GetAll()
        { 
            return await _context.Agreements.Select(a => new AgreementDtoOut
            {
                AgreementId = a.AgreementId,
                Title = a.Title,
                Description = a.Description,
                Image = a.Image,
                CategoryAgreementId = a.CategoryAgreementId,    
                CategoryName = a.CategoryAgreement.Description,
                IsActive = a.IsActive
            }).ToListAsync();
        }

        public async Task<IEnumerable<Agreement>> GetAllActiveAgreements()
        {
            return await _context.Agreements.Where(a => a.IsActive == true).ToListAsync();
           
        }




    }
}
