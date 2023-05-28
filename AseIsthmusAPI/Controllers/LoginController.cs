using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        private readonly LoginService _service;

        public LoginController(LoginService service)
        {
            _service = service;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {  
            var login = await _service.GetLogin(loginDto);
            if (login is null)
            return   BadRequest(new { message = "Credenciales inválidas" });

            return Ok( new {token = "some value"});
        }
    }
}
