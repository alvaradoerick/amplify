using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansTypeController : ControllerBase
    {
        private readonly LoansTypeService _service;

        public LoansTypeController(LoansTypeService service)
        {
            _service = service;
        }

        #region Get

        [HttpGet]
        public async Task<IEnumerable<LoansType>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("active-agreements")]
        public async Task<IEnumerable<LoansType>> GetAllActiveLoanTypes()
        {
            return await _service.GetAllActiveLoans();
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanTypeOutDto>> GetById([FromRoute] int id)
        {
            var agreement = await _service.GetById(id);

            if (agreement is null)
            {
                return NotFound(new { error = "No se pudo encontrar ninguna categoría." });
            }
            else
            {
                return agreement;
            }
        }

        #endregion

        #region Create

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(LoanTypeInDto loanType)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _service.Create(loanType);
                return Ok(loanType);
            }
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
                return BadRequest(new { error = "No se pudo eliminar el convenio." });
            }


        }

        #endregion

        #region Update
        // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] LoanTypeInDto loanType)
        {
            var loanTypeToUpdate = await _service.Update(id, loanType);

            if (loanTypeToUpdate is not null)
            {
                return NoContent();
            }
            else
            {
                return NotFound(new { error = "No se pudo actualizar el convenio." });
            }
        }
        #endregion
    }
}