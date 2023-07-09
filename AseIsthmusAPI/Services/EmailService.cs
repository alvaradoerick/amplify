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

        public void SendEmail(string emailTemplate, string subject, string emailTo, string pdfFilePath = null)
        {
            var email = new MimeMessage();
            // se pone el coorreo, en este caso, se uso de thereal https://ethereal.email/create
            email.From.Add(MailboxAddress.Parse("configurationAso@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailTemplate };

            if (!string.IsNullOrEmpty(pdfFilePath))
            {
                var attachment = new MimePart("application", "pdf")
                {
                    Content = new MimeContent(File.OpenRead(pdfFilePath), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(pdfFilePath)
                };

                var multipart = new Multipart("mixed")
                {
                    email.Body,
                    attachment
                };
                email.Body = multipart;
            }

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
