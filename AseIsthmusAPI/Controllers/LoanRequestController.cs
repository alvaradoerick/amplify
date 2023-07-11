using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "Loan-Approvers")]
        [HttpGet]
        public async Task<IEnumerable<LoanRequestOutDto>> Get()
        {
            return await _service.GetAll();
        }


        [Authorize(Policy = "Loan-Approvers")]
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanRequestOutDto>> GetById([FromRoute] int id)
        {
            var loan = await _service.GetById(id);

            if (loan is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
                return loan;
            }
        }
        #endregion

        #region Create

        [Authorize]
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

        #region Update

        [Authorize(Policy = "Loan-Approvers")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ApproveLoan([FromRoute] int id, [FromBody] LoanRequestInByAdminDto loan)
        {
            var loanToUpdate = await _service.ApproveLoan(id, loan);

            if (loanToUpdate is not null)
            {
                return NoContent();
            }
            else
            {
                return NotFound(new { error = "No se pudo actualizar el préstamo." });
            }
        }

        [Authorize(Policy = "Administrator")]
        [HttpPut("request-review/{id}")]
        public async Task<ActionResult> RequestLoanReview([FromRoute] int id)
        {
            var loan = await _service.GetById(id);

            if (loan is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
              await  _service.RequestLoanReview(id);
                return NoContent();
            }
        }

        //[Authorize(Policy = "Administrator")]
        [HttpPut("respond-review/{id}")]
        public async Task<ActionResult> RespondLoanReview([FromRoute] int id, [FromBody] bool response)
        {
            var loan = await _service.GetById(id);

            if (loan is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
               await _service.RespondLoanReview(id, response);
                return NoContent();
            }
        }
        #endregion

        #region methods

        [HttpPost("calculation")]
        public async Task<IActionResult> GetLoanCalculation([FromBody] LoanCalculationType loanCalculation)
        {
            var result = await _service.GetLoanCalculation(loanCalculation);


            return Ok(result);
        }

        #endregion

        #region Delete

        [Authorize(Policy = "Loan-Approvers")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest(new { error = "No se pudo eliminar el préstamo." });
            }
        }
        #endregion
    }
}
