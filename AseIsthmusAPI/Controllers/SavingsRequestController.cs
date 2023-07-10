using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Agreement;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingsRequestController : ControllerBase
    {
        private readonly SavingsRequestService _service;

        public SavingsRequestController(SavingsRequestService service)
        {
            _service = service;
        }

        #region Get

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public async Task<IEnumerable<SavingsRequestOutDto>> Get()
        {
            return await _service.GetAll();
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SavingsRequestOutDto>> GetById([FromRoute] int id)
        {
            var savings = await _service.GetById(id);

            if (savings is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun ahorro con ese ID." });
            }
            else
            {
                return savings;
            }
        }
        #endregion

        #region Create

        [Authorize(Policy = "Administrator")]
        [HttpPost("{id}")]
        public async Task<IActionResult> Create([FromRoute]string id, SavingsRequestInDto savings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _service.Create(id, savings);
                return Ok(savings);
            }
        }
        #endregion

        #region Update

        [Authorize(Policy = "Administrator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ApproveSavings([FromRoute] int id, [FromBody]SavingsRequestInByAdminDto savings)
        {
            var savingToUpdate = await _service.ApproveSaving(id, savings);

            if (savingToUpdate is not null)
            {
                return NoContent();
            }
            else
            {
                return NotFound(new { error = "No se pudo actualizar el ahorro." });
            }
        }
        #endregion

        #region Delete

        [Authorize(Policy = "Administrator")]
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
                return BadRequest(new { error = "No se pudo eliminar el ahorro." });
            }
        }
        #endregion
    }
}
