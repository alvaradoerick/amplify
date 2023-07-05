using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanRequestController : ControllerBase
    {

        private readonly LoanRequestService _service;

        public LoanRequestController(LoanRequestService service)
        {
            _service = service;
        }
        #region Get

        [HttpGet("calculation")]
        public ActionResult<sp_GetLoanCalculation_Result> GetLoanCalculation(string personId, int loanTypeId, int term, decimal amount)
        {
            var result = _service.GetLoanCalculation(personId, loanTypeId, term, amount);


            return Ok(result.Result);
        }
        #endregion

        #region Create

        // [Authorize]
        [HttpPost("{id}")]
    public async Task<IActionResult> Create([FromRoute] string id, LoanRequestInDto loan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        else
        {
            await _service.Create(id, loan);
            return Ok(loan);
        }
    }
    #endregion
}
}
