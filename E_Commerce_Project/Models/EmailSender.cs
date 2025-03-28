using System.Net.Mail;
using System.Net;
using E_Commerce_Project.Utility;

namespace E_Commerce_Project.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("worldtroidgaming@gmail.com", "hmlx apkn iapv ctid")
            };

            return client.SendMailAsync(
                new MailMessage(from: "worldtroidgaming@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {
                    IsBodyHtml = true

                }

                );

        }
    }

}
