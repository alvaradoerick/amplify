using Microsoft.AspNetCore.Mvc;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet]
        public async Task<IEnumerable<AgreementDtoOut>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] int id)
        {
            var agreement = await _service.GetAgreementByCategoryId(id);

            if (!agreement.Any() )
            {
                return NoContent();
            }
            else
            {
                return Ok(agreement);  
            }           
        }

    }
}
