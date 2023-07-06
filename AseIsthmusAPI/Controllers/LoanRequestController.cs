using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
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
        [HttpGet]
        public async Task<IEnumerable<LoanRequestOutDto>> Get()
        {
            return await _service.GetAll();
        }
       

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanRequestOutDto>> GetById([FromRoute] int id)
        {
            var savings = await _service.GetById(id);

            if (savings is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun préstamo con ese ID." });
            }
            else
            {
                return savings;
            }
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

        #region Update

        // [Authorize]
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

        [HttpPost("calculation")]
        public async Task<IActionResult> GetLoanCalculation([FromBody] LoanCalculationType loanCalculation)
        {
            var result = await _service.GetLoanCalculation(loanCalculation);


            return Ok(result);
        }

        #endregion

        #region Delete

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
