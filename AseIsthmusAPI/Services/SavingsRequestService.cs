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
               .OrderByDescending(a => a.IsApproved == null)

                .ThenByDescending(a => a.IsActive)
                .Select(a => new SavingsRequestOutDto
                    {
                     SavingsRequestId = a.SavingsRequestId,
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
              
                existingSaving.IsApproved = saving.IsApproved;            
                if (saving.IsApproved == false) {
                    existingSaving.ApprovedDate = null;
                    existingSaving.IsActive = false;
                }
                else {
                    existingSaving.ApprovedDate = DateTime.Now;
                    existingSaving.IsActive = true;
                }
               
               
               

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

        public async Task Delete(int id)
        {
            var savingsToDelete = await _context.SavingsRequests.Where(a => a.SavingsRequestId == id).FirstOrDefaultAsync();

            if (savingsToDelete is not null)
            {
                _context.SavingsRequests.Remove(savingsToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
