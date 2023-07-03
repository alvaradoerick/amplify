using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IEnumerable<SavingsRequestOutDto>> Get()
        {
            return await _service.GetAll();
        }
        #endregion

        #region Create

        // [Authorize]
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
    }
}
