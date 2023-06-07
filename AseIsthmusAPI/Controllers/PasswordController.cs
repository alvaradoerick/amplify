using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly PasswordService _service;
        private IConfiguration config;

        public PasswordController(PasswordService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] string id, List<Login> login)
        {
            return null;

        }

        [Authorize]
        [HttpPatch("setNewPassword")]
        public async Task<IActionResult> SetPassword(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            var newPassword = await _service.SetNewPassword(updatePasswordRequestDto);
            if (newPassword != null)
            {
                return Ok(new UpdatePasswordResponseDto { NewPassword = newPassword });
            }
            else
            {
                return BadRequest(new { error = "Su solicitud no pudo enviarse." });
            }
        }

        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPasswordUnauthenticated(UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            var newPassword = await _service.ResetPasswordUnauthenticated(updatePasswordRequestDto);
            if (newPassword != null && newPassword != "1")
            {
                return Ok(new UpdatePasswordResponseDto { NewPassword = newPassword });
            }
            else if (newPassword == "1")
            {
                return BadRequest(new { error = "Su afiliación no está activada." });
            }
            else
            {
                return BadRequest(new { error = "Su solicitud no pudo enviarse." });
            }
        }
    }
}
