using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{
    [Authorize]
    [Route("api/users/{id}/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] string id, List<Login> login)
        {
            return null;

        }
    }
}
