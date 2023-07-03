using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Data;

namespace AseIsthmusAPI.Services
{
    public class LoanRequestService
    {
        private readonly AseItshmusContext _context;
        public LoanRequestService(AseItshmusContext context)
        {
            _context = context;
        }

        public async Task<LoanRequest> Create(string id, LoanRequestInDto loanRequest)
        {
            var savings = new LoanRequest
            {
                PersonId = id,
                LoansTypeId = loanRequest.LoansTypeId,
                AmountRequested = loanRequest.AmountRequested,
                Term = loanRequest.Term,
                BankAccount = loanRequest.BankAccount,
                RequestedDate = loanRequest.RequestedDate,
                IsActive = false,
            };

            _context.LoanRequests.Add(savings);
            await _context.SaveChangesAsync();

            return savings;
        }
    }
}
