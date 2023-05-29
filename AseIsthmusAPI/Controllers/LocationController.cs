using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;
        public LocationController(LocationService service)
        {
            _service = service;
        }

        [HttpGet("province/{provinceId}/cantons")]
        public async Task<ActionResult<List<Canton>>> GetCantonsByProvince(int provinceId)
        {
            var cantons = await _service.GetCantonsByProvince(provinceId);
            return Ok(cantons);
        }

        [HttpGet("canton/{cantonId}/districts")]
        public async Task<ActionResult<List<District>>> GetDistrictsByCanton(int cantonId)
        {
            var districts = await _service.GetDistrictsByCanton(cantonId);
            return Ok(districts);
        }
    }

}
