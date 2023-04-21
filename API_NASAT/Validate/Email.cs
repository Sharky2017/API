using System.Net;
using System.Net.Mail;

namespace API_NASAT.Validate
{
    public static class Email
    {

        public static bool SendEmail(string email, string password, string code, string address)
        {

            string subject = "Validar su registro en NASA Tecnologia";
            string message = "<img src='https://nasa.com.mx/wp-content/uploads/logotipo-nasa-tecnologia-blanco@2x.png' ><h2>Este es su codigo clave para validar su registro</h2><br><br><div style='text-align:center'><i style='background-color:red;padding: 15px; color:#ffffff'>" + code + "</i></div>";


            try
            {
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(address));
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);

                return true;
            }
            catch
            {
                return false;

            }

        }
    }
}
