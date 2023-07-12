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

        public string ApprovalEmailContent()
        {
            var content = new StringBuilder();

            content.AppendLine("<div>");
            content.AppendLine("    <p>Hola,</p>");
            content.AppendLine("    <p>Tu solicitud para ingresar a AseIsthmus ha sido aprobada.</p>");
            content.AppendLine("    <p>Por favor ingrese a nuestro sitio web para poder conocer nuestros beneficios.</p>" +
                "</br>");
            content.AppendLine("    <p>Adjunto podrás encontrar nuestro contrato. Por favor fírmelo y envienoslo de vuelta al correo: <strong>correo@correo.com</strong>.</p> </br>");
            content.AppendLine("    <p>Te recomendamos cambiar la contraseña después de iniciar sesión.</p>");
            content.AppendLine("    <p>Si tienes alguna pregunta o necesitas asistencia adicional, no dudes en contactarnos.</p>");
            content.AppendLine("    <p>¡Gracias!</p>");
            content.AppendLine("</div>");

            return content.ToString();
        }

        public string PagareEmailContent(string associateName)
        {

            var content = new StringBuilder();

            content.AppendLine("<div>");
            content.AppendLine($"    <p>Hola {associateName}</p>");
            content.AppendLine("    <p>Adjunto encontrará el documento del Pagaré, el cual requiere su atención y completar los campos correspondientes.</p>");
            content.AppendLine("    <p>Le agradeceríamos que tomara unos minutos para llenar los datos necesarios en el documento adjunto y nos lo enviara de vuelta.</p>");
            content.AppendLine("    <p>Si tiene alguna pregunta o necesita asistencia adicional, no dude en contactarnos. Estamos aquí para ayudarle.</p>");
            content.AppendLine("    <p>Esperamos recibir el Pagaré completado a la brevedad posible</p>");
            content.AppendLine("    <p>Saludos cordiales,</p>");
            content.AppendLine("</div>");

            return content.ToString();
        }

        public string LoanRejectedEmailContent(string associateName)
        {

            var content = new StringBuilder();

            content.AppendLine("<div>");
            content.AppendLine($"    <p>Hola {associateName}</p>");
            content.AppendLine("    <p>Lamentamos informarle que su solicitud de préstamo no ha sido aprobada. Comprendemos que esto pueda generarle preocupación o dudas, por lo que estamos aquí para brindarle toda la información que necesite.</p>");
            content.AppendLine("    <p>Queremos recordarle que la aprobación de un préstamo está sujeta a diversos factores, como historial crediticio, capacidad de pago y políticas internas de la institución financiera. Aunque su solicitud no haya sido aprobada en esta ocasión, le animamos a explorar otras alternativas o a mejorar su perfil crediticio para futuras oportunidades.</p>");
            content.AppendLine("    <p>Si tiene alguna pregunta o necesita aclarar algún aspecto en particular, no dude en ponerse en contacto con nosotros.</p>");
            content.AppendLine("    <p>Saludos cordiales,</p>");
            content.AppendLine("</div>");

            return content.ToString();
        }
    }
}
