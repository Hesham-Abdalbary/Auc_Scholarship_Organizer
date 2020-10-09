using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Auc_Scholarship_Organizer.Services
{
    public class EmailService
    {
        public void SendEmail(string title, string body, string toEmail)
        {
            var fromAddress = new MailAddress("scholarshipapproval.hesham@gmail.com", "Scholarship Team");
            var toAddress = new MailAddress(toEmail, "Student");
            string fromPassword = "12SNowow";
            string subject = title;
            string emailBody = body;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = emailBody
            })
            {
                smtp.Send(message);
            }
        }

    }
}