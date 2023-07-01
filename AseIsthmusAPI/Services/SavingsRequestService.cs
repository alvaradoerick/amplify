using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AseIsthmusAPI.Services
{
    public class SavingsRequestService
    {
        private readonly AseItshmusContext _context;
        public SavingsRequestService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<SavingsRequest> Create(string id, SavingsRequestInDto savingsRequest)
        {
            var savings = new SavingsRequest
            {
                PersonId = id,
                SavingsTypeId = savingsRequest.SavingsTypeId,
                Amount = savingsRequest.Amount,
                ApplicationDate = DateTime.Now,
                IsActive = false,
            };

            _context.SavingsRequests.Add(savings);
            await _context.SaveChangesAsync();

            return savings;
        }
    }
}
