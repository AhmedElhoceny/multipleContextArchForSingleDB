using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SharedHelpers.HelperServices
{
    public static class MailingService
    {
        public static string username = "";
        public static string password = "";
        public static string senderEmail = "email-smtp.us-east-1.amazonaws.com";
        public static string host = "";
        public static int port = 25;
        public static async Task<bool> SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username,password)
                };
                using (var message = new MailMessage(senderEmail, email)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    message.From = new MailAddress(senderEmail, email.Split('@')[0]);
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
