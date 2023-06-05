using Microsoft.AspNetCore.Mvc;
using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Agreement agreement)
        {

            if (agreement.Image is not null && agreement.Image.Length > 0)
            {
                var newAgreement = await _service.Create(agreement);

                return Ok(newAgreement);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
