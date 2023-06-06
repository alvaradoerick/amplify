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
        public IActionResult Create(AgreementDtoIn agreementDtoIn)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                _service.Create(agreementDtoIn);
                return Ok("Sucess");
            }          
        }

    }
}
