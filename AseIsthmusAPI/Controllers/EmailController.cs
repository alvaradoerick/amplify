using AseIsthmusAPI.Data.DTOs;
using AseIsthmusAPI.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace AseIsthmusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _service;
        public EmailController(EmailService service)
        {
            _service = service;
        }
        [HttpPost("resetPassword")]
        public IActionResult SendEmail(EmailDto emailDto) {
            _service.SendEmail(emailDto);
            return Ok();
        }
    }
}
