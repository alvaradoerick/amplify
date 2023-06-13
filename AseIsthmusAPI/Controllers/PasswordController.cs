using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using AseIsthmusAPI.Templates;
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
        private readonly EmailService _emailService;

        public PasswordController(PasswordService service, EmailService emailService)
        {
            _service = service;
            _emailService = emailService;
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

        /// <summary>
        /// resets the password of an active user without having to log in
        /// </summary>
        /// <param name="updatePasswordRequestDto"></param>
        /// <returns></returns>
        [HttpPatch("resetPassword")]
        public async Task<IActionResult> ResetPasswordUnauthenticated( [FromBody]UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            HtmlContentProvider emailTemplate = new HtmlContentProvider();


            var newPassword = await _service.ResetPasswordUnauthenticated(updatePasswordRequestDto);
            if (newPassword != null && newPassword != "1")
            {
                
                
                _emailService.SendEmail(emailTemplate.GeneratePasswordResetEmailContent(newPassword), "Restablecimiento de contraseña", updatePasswordRequestDto.EmailAddress);
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
