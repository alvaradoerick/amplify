using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;

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

        public async Task<sp_GetLoanCalculation_Result> GetLoanCalculation(string personId, int loanTypeId, int term, decimal amount)
        {
            var personIdParameter = new SqlParameter("@personId", personId);
            var loanTypeIdParameter = new SqlParameter("@loanTypeId", loanTypeId);
            var termParameter = new SqlParameter("@term", term);
            var amountParameter = new SqlParameter("@amount", amount);
            var availEmployeeAmtParameter = new SqlParameter("@availEmployeeAmt", SqlDbType.Decimal) { Direction = ParameterDirection.Output, Precision = 18, Scale = 2 };
            var availEmployerAmtParameter = new SqlParameter("@availEmployerAmt", SqlDbType.Decimal) { Direction = ParameterDirection.Output, Precision = 18, Scale = 2 };
            var totalAvailAmountParameter = new SqlParameter("@totalAvailAmount", SqlDbType.Decimal) { Direction = ParameterDirection.Output, Precision = 18, Scale = 2 };
            var biweeklyFeeParameter = new SqlParameter("@biweeklyFee", SqlDbType.Decimal) { Direction = ParameterDirection.Output, Precision = 18, Scale = 2 };
            var totalAmtPayParameter = new SqlParameter("@totalAmtPay", SqlDbType.Decimal) { Direction = ParameterDirection.Output, Precision = 18, Scale = 2 };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_GetLoanCalculation @personId, @loanTypeId, @term, @amount, @availEmployeeAmt OUTPUT, @availEmployerAmt OUTPUT, @totalAvailAmount OUTPUT, @biweeklyFee OUTPUT, @totalAmtPay OUTPUT",
                personIdParameter,
                loanTypeIdParameter,
                termParameter,
                amountParameter,
                availEmployeeAmtParameter,
                availEmployerAmtParameter,
                totalAvailAmountParameter,
                biweeklyFeeParameter,
                totalAmtPayParameter
            );

            var loanCalculationResult = new sp_GetLoanCalculation_Result
            {
                AvailEmployeeAmt = availEmployeeAmtParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)availEmployeeAmtParameter.Value,
                AvailEmployerAmt = availEmployerAmtParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)availEmployerAmtParameter.Value,
                TotalAvailAmount = totalAvailAmountParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)totalAvailAmountParameter.Value,
                BiweeklyFee = biweeklyFeeParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)biweeklyFeeParameter.Value,
                TotalAmtToPay = totalAmtPayParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)totalAmtPayParameter.Value
            };

            return loanCalculationResult;

        }
    }
}
