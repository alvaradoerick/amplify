using AseIsthmusAPI.Data;
using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Xml.XPath;


namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginService _service;
        private IConfiguration config;

        public LoginController(LoginService service, IConfiguration config)
        {
            _service = service;
            this.config = config;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var login = await _service.GetLogin(loginDto);
            if (login is null)
                return BadRequest(new { message = "Credenciales inválidas" });

            string jwtToken = await GenerateToken(login.Person);
            return Ok(new { token = jwtToken });
        }

        private async Task<string> GenerateToken(User user)
        {
            //var roleDescription = await _roleService.GetRoleDescriptionById(user.RoleId);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.FirstName),
        new Claim(ClaimTypes.Email, user.EmailAddress),
       // new Claim("RoleType", await roleDescription)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

        [Authorize]
        [HttpPatch("setNewPassword")]
        public async Task<IActionResult> SetPassword(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            try
            {
                var newPassword = await _service.UpdatePasswordByEmail(updatePasswordRequestDto);
                return Ok(new UpdatePasswordResponseDto { NewPassword = newPassword });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPassword(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            try
            {
                var newPassword = await _service.ResetPasswordByEmail(updatePasswordRequestDto);
                return Ok(new UpdatePasswordResponseDto { NewPassword = newPassword });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
