using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using AseIsthmusAPI.Data.DTOs;
using MailKit.Net.Smtp;
using AseIsthmusAPI.Templates;

namespace AseIsthmusAPI.Services
{
    public class EmailService
    {
       private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public void SendEmail(string emailTemplate,string subject, string emailTo) {

            var email = new MimeMessage();
            // se pone el coorreo, en este caso, se uso de thereal https://ethereal.email/create
            email.From.Add(MailboxAddress.Parse("krudin7.6@gmal.com"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailTemplate};


            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);// smtp.Connect("smtp.gmail.com")
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
