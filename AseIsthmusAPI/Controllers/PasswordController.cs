using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using AseIsthmusAPI.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AseIsthmusAPI.Controllers
{

    [Route("api/[controller]/resetpassword")]
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

        #region Authenticated update
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> ResetPasswordAuthenticated([FromRoute] string id, [FromBody] ResetPasswordDto resetPassword)
        {
            HtmlContentProvider emailTemplate = new HtmlContentProvider();
            if (resetPassword == null) {
                return BadRequest(new { error = "La contraseña es requerida." });
            }
            var newPasswordResponse = await _service.ResetPasswordAuthenticated(id, resetPassword);
            if (newPasswordResponse.Item1 != null)
            {
                _emailService.SendEmail(emailTemplate.GeneratePasswordResetEmailContent(newPasswordResponse.Item1), "Restablecimiento de contraseña", newPasswordResponse.Item2);
                return Ok(new UpdatePasswordResponseDto { NewPassword = newPasswordResponse.Item1 });
            }
            else
            {
                return BadRequest(new { error = "Su solicitud no pudo enviarse." });
            }
        }
        #endregion

        #region Unauthenticated Update
        /// <summary>
        /// resets the password of user without having to log in
        /// </summary>
        /// <param name="updatePasswordRequestDto"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> ResetPasswordUnauthenticated([FromBody] UpdatePasswordRequestDto updatePasswordRequestDto)
        {
            HtmlContentProvider emailTemplate = new HtmlContentProvider();

            var newPassword = await _service.ResetPasswordUnauthenticated(updatePasswordRequestDto);
            if (newPassword != null)
            {
                _emailService.SendEmail(emailTemplate.GeneratePasswordResetEmailContent(newPassword), "Restablecimiento de contraseña", updatePasswordRequestDto.EmailAddress);
                return Ok(new UpdatePasswordResponseDto { NewPassword = newPassword });
            }
            else
            {
                return BadRequest(new { error = "Su solicitud no pudo enviarse." });
            }
        }

        /// <summary>
        /// resets the password of user after logged in
        /// </summary>
        /// <param name="updatePasswordRequestDto"></param>
        /// <returns></returns>
        #endregion
    }
}
