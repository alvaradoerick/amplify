using AseIsthmusAPI.Data.AseIsthmusModels;
using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using AseIsthmusAPI.Templates;
using Microsoft.AspNetCore.Authorization;
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

        #region Authenticated update

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] string id, List<Login> login)
        {
            return null;

        }
        #endregion

        #region Unauthenticated Update
        /// <summary>
        /// resets the password of user without having to log in
        /// </summary>
        /// <param name="updatePasswordRequestDto"></param>
        /// <returns></returns>
        [HttpPatch("resetpassword")]
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

        #endregion
    }
}
