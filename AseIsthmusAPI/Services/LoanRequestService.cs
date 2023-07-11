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
        public async Task<IEnumerable<LoanRequestOutDto>> GetAll()
        {
            return await _context.LoanRequests
               .OrderByDescending(a => a.IsApproved == null)
                .ThenByDescending(a => a.IsActive)
                .Select(a => new LoanRequestOutDto
                {
                    LoanRequestId = a.LoanRequestId,
                    PersonId = a.PersonId,
                    Name = $"{a.Person.FirstName} {a.Person.LastName1} {a.Person.LastName2}",
                    NumberId = a.Person.NumberId,
                    LoansTypeId = a.LoansTypeId,
                    LoanTypeName = a.LoansType.Description,
                    RequestedDate = a.RequestedDate,
                    AmountRequested = a.AmountRequested,
                    Term = a.Term,
                    BankAccount = a.BankAccount,
                    IsActive = a.IsActive,
                    ApprovedDate = a.ApprovedDate,
                    IsApproved = a.IsApproved,
                    IsReviewRequired=  a.IsReviewRequired,
                    IsReviewApproved = a.IsReviewApproved,
                    ReviewRequiredDate = a.ReviewRequiredDate
    }).ToListAsync();
        }
        public async Task<LoanRequestOutDto?> GetById(int id)
        {
            return await _context.LoanRequests.Where(a => a.LoanRequestId == id).
                Select(a => new LoanRequestOutDto
                {
                    LoanRequestId = a.LoanRequestId,
                    PersonId = a.PersonId,
                    Name = $"{a.Person.FirstName} {a.Person.LastName1} {a.Person.LastName2}",
                    NumberId = a.Person.NumberId,
                    LoansTypeId = a.LoansTypeId,
                    LoanTypeName = a.LoansType.Description,
                    RequestedDate = a.RequestedDate,
                    AmountRequested = a.AmountRequested,
                    Term = a.Term,
                    BankAccount = a.BankAccount,
                    IsActive = a.IsActive,
                    ApprovedDate = a.ApprovedDate,
                    IsApproved = a.IsApproved,
                    IsReviewRequired = a.IsReviewRequired,
                    IsReviewApproved = a.IsReviewApproved,
                    ReviewRequiredDate = a.ReviewRequiredDate
                }).SingleOrDefaultAsync();
        }

        public async Task RequestLoanReview(int loanRequestId)
        {
            var loanIdParameter = new SqlParameter("@loanId", SqlDbType.Int)
            {
                Value = loanRequestId
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RequestLoanReview @loanId",
                loanIdParameter
            );

        }

        public async Task RespondLoanReview(int loanRequestId, bool response )
        {
            var loanIdParameter = new SqlParameter("@loanId", SqlDbType.Int)
            {
  
                Value = loanRequestId
            };
            var reviewResponseParameter = new SqlParameter("@reviewResponse", SqlDbType.Bit)
            {
                Value = response
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateReviewResponse @loanId, @reviewResponse",
                loanIdParameter,
                reviewResponseParameter
            );

        }


        public async Task<LoanRequest> ApproveLoan(int id, LoanRequestInByAdminDto saving)
        {
            var existingLoan = await _context.LoanRequests.FindAsync(id);

            if (existingLoan is not null)
            {

                existingLoan.IsApproved = saving.IsApproved;
                if (saving.IsApproved == false)
                {
                    existingLoan.ApprovedDate = null;
                    existingLoan.IsActive = false;
                }
                else
                {
                    existingLoan.ApprovedDate = DateTime.Now;
                    existingLoan.IsActive = true;
                }




                await _context.SaveChangesAsync();
                return existingLoan;
            }
            else return null;
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

        public async Task<sp_GetLoanCalculation_Result> GetLoanCalculation(LoanCalculationType loanCalculation)
        {
            var loanData = ConvertLoanCalculationTypeToDataTable(loanCalculation);

            var loanDataParameter = new SqlParameter("@loanData", SqlDbType.Structured)
            {
                TypeName = "dbo.LoanCalculationTypes",
                Value = loanData
            };
            var availEmployeeAmtParameter = new SqlParameter("@availEmployeeAmt", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };
            var availEmployerAmtParameter = new SqlParameter("@availEmployerAmt", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };
            var totalAvailAmountParameter = new SqlParameter("@totalAvailAmount", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };
            var biweeklyFeeParameter = new SqlParameter("@biweeklyFee", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };
            var totalAmtPayParameter = new SqlParameter("@totalAmtPay", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };

            var rateParameter = new SqlParameter("@rate", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output,
                Precision = 18,
                Scale = 2
            };


            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_GetLoanCalculation @loanData, @availEmployeeAmt OUTPUT, @availEmployerAmt OUTPUT, @totalAvailAmount OUTPUT, @biweeklyFee OUTPUT, @totalAmtPay OUTPUT, @rate OUTPUT",
                loanDataParameter,
                availEmployeeAmtParameter,
                availEmployerAmtParameter,
                totalAvailAmountParameter,
                biweeklyFeeParameter,
                totalAmtPayParameter,
                rateParameter
            );

            var loanCalculationResult = new sp_GetLoanCalculation_Result
            {
                AvailEmployeeAmt = availEmployeeAmtParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)availEmployeeAmtParameter.Value,
                AvailEmployerAmt = availEmployerAmtParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)availEmployerAmtParameter.Value,
                TotalAvailAmount = totalAvailAmountParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)totalAvailAmountParameter.Value,
                BiweeklyFee = biweeklyFeeParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)biweeklyFeeParameter.Value,
                TotalAmtToPay = totalAmtPayParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)totalAmtPayParameter.Value,
                Rate = rateParameter.Value.Equals(DBNull.Value) ? 0m : (decimal)rateParameter.Value

            };

            return loanCalculationResult;

        }

        public async Task Delete(int id)
        {
            var loanToDelete = await _context.LoanRequests.Where(a => a.LoanRequestId == id).FirstOrDefaultAsync();

            if (loanToDelete is not null)
            {
                _context.LoanRequests.Remove(loanToDelete);
                await _context.SaveChangesAsync();
            }
        }

        private DataTable ConvertLoanCalculationTypeToDataTable(LoanCalculationType loanCalculation)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("PersonId", typeof(string));
            dataTable.Columns.Add("LoansTypeId", typeof(int));
            dataTable.Columns.Add("Term", typeof(int));
            dataTable.Columns.Add("Amount", typeof(decimal));

                DataRow row = dataTable.NewRow();
                row["PersonId"] = loanCalculation.PersonId;
                row["LoansTypeId"] = loanCalculation.LoansTypeId;
                row["Term"] = loanCalculation.Term;
                row["Amount"] = loanCalculation.Amount;

                dataTable.Rows.Add(row);
  

            return dataTable;
        }
    }
}
