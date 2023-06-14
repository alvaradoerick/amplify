using Microsoft.AspNetCore.Html;
using System.Text;

namespace AseIsthmusAPI.Templates
{
    public class HtmlContentProvider
    {

        public string GeneratePasswordResetEmailContent(string newPassword)
        {
            var content = new StringBuilder();

            content.AppendLine("<div>");
            content.AppendLine("    <p>Hola,</p>");
            content.AppendLine("    <p>Tu contraseña ha sido restablecida correctamente.</p>");
            content.AppendLine("    <p>A continuación, te proporcionamos la nueva contraseña:</p>");
            content.AppendLine($"    <p><strong>{newPassword}</strong></p>");
            content.AppendLine("    <p>Te recomendamos cambiar la contraseña después de iniciar sesión.</p>");
            content.AppendLine("    <p>Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en contactarnos.</p>");
            content.AppendLine("    <p>¡Gracias!</p>");
            content.AppendLine("</div>");

            return content.ToString();
        }
    }
}
