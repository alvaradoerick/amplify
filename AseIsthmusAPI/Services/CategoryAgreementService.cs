using Microsoft.EntityFrameworkCore;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using System.Net;

namespace AseIsthmusAPI.Services
{
    public class CategoryAgreementService
    {

        private readonly AseItshmusContext _context;

        public CategoryAgreementService (AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryAgreement>> GetAll()
        {
            return await _context.CategoryAgreements.ToListAsync();
        }
        public async Task<CategoryAgreement?> GetById(int id)
        {
            return await _context.CategoryAgreements.FindAsync(id);
        }
        public async Task<IEnumerable<CategoryAgreement>> GetAllActiveCategories()
        {
            var activeCats =  await _context.CategoryAgreements.Where(a=>a.IsActive == true).ToListAsync();

            return activeCats;  
        }
        public async Task<CategoryAgreement> Create(CategoryAgreement newCategoryAgreement)
        {
            _context.CategoryAgreements.Add(newCategoryAgreement);
            await _context.SaveChangesAsync();

            return newCategoryAgreement;
        }
        public async Task Update(int id, CategoryAgreement categoryAgreement)
        {
            var existingCategoryAgreement = await GetById(id);

            if (existingCategoryAgreement is not null)
            {
                existingCategoryAgreement.Description = categoryAgreement.Description;
                existingCategoryAgreement.IsActive = categoryAgreement.IsActive;

                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var categoryAgreementToDelete = await GetById(id);

            if (categoryAgreementToDelete is not null)
            {
                _context.CategoryAgreements.Remove(categoryAgreementToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> HasAgreements(int id)
        {
            var agreementsCount = await _context.Agreements.Where(a => a.CategoryAgreementId == id).CountAsync();

            return agreementsCount > 0;
        }
    }
}
