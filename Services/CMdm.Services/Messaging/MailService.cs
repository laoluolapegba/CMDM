using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMdm.Services.Messaging
{
    public class MailService
    {
        public void SendMail(string[] addresses, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.Subject = subject;
                mail.From = new MailAddress("sirlamlanre@gmail.com", "Salam Lanre");
                foreach (var address in addresses)
                {
                    mail.To.Add(new MailAddress(address));
                }
                mail.Body = body;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                var networkCred = new System.Net.NetworkCredential("sirlamlanre", "my_password", "domaiin");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCred;
                smtp.Port = 465;

                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                (s, cert, chain, sslPolicyErrors) => true;
                smtp.Send(mail);
            }
            catch (SmtpException ste)
            {
                Console.WriteLine(ste.ToString());
            }

        }
    }
}
