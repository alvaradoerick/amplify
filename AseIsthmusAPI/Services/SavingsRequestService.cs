using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Agreement;

namespace AseIsthmusAPI.Services
{
    public class SavingsRequestService
    {
        private readonly AseItshmusContext _context;
        public SavingsRequestService(AseItshmusContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<SavingsRequestOutDto>> GetAll()
        {
            return await _context.SavingsRequests
                .OrderBy(a => a.IsApproved == null)  
                .ThenBy(a => a.IsApproved)
                .Select(a => new SavingsRequestOutDto
                    {
                        PersonId = a.PersonId,
                        Name = $"{a.Person.FirstName} {a.Person.LastName2}",
                        NumberId = a.Person.NumberId,
                        SavingsTypeId = a.SavingsTypeId,
                        SavingsTypeName = a.SavingsType.Description,
                        ApplicationDate = a.ApplicationDate,
                        Amount = a.Amount,
                        IsActive = a.IsActive,
                        ApprovedDate = a.ApprovedDate,
                        IsApproved = a.IsApproved
                    }).ToListAsync();
        }

        public async Task<SavingsRequest> ApproveSaving(int id, SavingsRequestInByAdminDto saving) {
            var existingSaving = await _context.SavingsRequests.FindAsync(id);

            if (existingSaving is not null)
            {
                existingSaving.IsActive = saving.IsActive;
                existingSaving.ApprovedDate = saving.ApprovedDate;
                existingSaving.IsApproved = saving.IsApproved;

                await _context.SaveChangesAsync();
                return existingSaving;
            }
            else return null;
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
