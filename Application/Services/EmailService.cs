using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string to, string subject, string body) 
        {
            //smtp client - simple mail transfer protocole
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("morgo200420@gmail.com", "etvt mocp lkkn sizp");
            smtpClient.EnableSsl = true;


            //new message send
            MailMessage mailMessage = new MailMessage("morgo200420@gmail.com", to, subject, body);

            smtpClient.Send(mailMessage);
        }
    }
}
