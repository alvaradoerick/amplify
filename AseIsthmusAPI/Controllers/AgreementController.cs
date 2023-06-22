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

        #endregion

        #region Create

        // [Authorize]
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

    }
}
