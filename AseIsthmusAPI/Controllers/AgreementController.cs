using Microsoft.AspNetCore.Mvc;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using AseIsthmusAPI.Data.AseIsthmusModels;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly AgreementService _service;

        public AgreementController(AgreementService service)
        {
            _service = service;
        }

        #region Get

        [HttpGet]
        public async Task<IEnumerable<AgreementDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("active-agreements")]
        public async Task<IEnumerable<Agreement>> GetAllActiveAgreements()
        {
            return await _service.GetAllActiveAgreements();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AgreementDtoOut>> GetById([FromRoute] int id)
        {
            var agreement = await _service.GetById(id);

            if (agreement is null)
            {
                return NotFound(new { error = "No se pudo encontrar ningun convenio." });
            }
            else
            {
                return agreement;
            }
        }

        #endregion

        #region Create

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AgreementDtoIn agreementDtoIn)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _service.Create(agreementDtoIn);
                return Ok(agreementDtoIn);
            }
        }
        #endregion

        #region Delete
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try {
                await _service.Delete(id);
                return NoContent();
            } catch (Exception) {
                return BadRequest(new { error = "No se pudo eliminar el convenio." });
            }
        
            
        }

        #endregion

        #region Update
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] AgreementDtoIn agreement)
        {
            var agreementToUpdate = await _service.Update(id, agreement);

            if (agreementToUpdate is not null)
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
