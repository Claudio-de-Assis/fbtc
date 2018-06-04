using System.Net.Mail;
using System.Net;
using System;

namespace Fbtc.Infra.Helpers
{
    public class SendEMail
    {
        private readonly string hostName;
        private readonly int portNumber;
        private readonly bool enableSSL;
        private readonly bool useDefaultCredentials;
        private readonly string userCredential;
        private readonly string passwordCredential;
        private readonly string sender;
        private readonly bool isDebug;

        public SendEMail()
        {
            isDebug = Convert.ToBoolean(ConfigHelper.GetKeyAppSetting("IsDebug"));

            hostName = ConfigHelper.GetKeyAppSetting("HostName");
            portNumber = Convert.ToInt32(ConfigHelper.GetKeyAppSetting("PortNumber"));
            enableSSL = Convert.ToBoolean(ConfigHelper.GetKeyAppSetting("enableSSL")); 
            useDefaultCredentials = true;
            userCredential = ConfigHelper.GetKeyAppSetting("UserCredential");
            passwordCredential = ConfigHelper.GetKeyAppSetting("PassWordCredential");
            sender = ConfigHelper.GetKeyAppSetting("Sender");
        }

        public bool SendMessage(string recipiente, string subject, bool isBodyHtml, string textBody)
        {
            bool _resultado = false;

            try
            {
                // SMTP Client Details:
                SmtpClient clientDetails = new SmtpClient();
                clientDetails.Port = isDebug == false ? portNumber : 25;
                clientDetails.Host = isDebug == false ? hostName : "localhost"  ;
                clientDetails.EnableSsl = isDebug == false ? enableSSL : false;
                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = useDefaultCredentials;
                clientDetails.Credentials = new NetworkCredential(userCredential, passwordCredential);

                // Message Details:
                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress(sender);
                mailDetails.To.Add(recipiente);
                mailDetails.Subject = subject;
                mailDetails.IsBodyHtml = isBodyHtml;
                mailDetails.Body = textBody;

                clientDetails.Send(mailDetails);

                _resultado = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro na tentativa de envio de e-mail:{ex.GetType()}. Erro:{ex.Message}");
            }
            return _resultado;
        }
    }
}
