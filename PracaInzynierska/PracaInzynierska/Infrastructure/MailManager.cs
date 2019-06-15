using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PracaInzynierska.Infrastructure
{
    public class MailManager : IMyEmailSender
    {
        private IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {         
            var smtpClient = new SmtpClient
            {
                Host = _configuration.GetSection("EmailSettings")["Host"],
                Port = Convert.ToInt32(_configuration.GetSection("EmailSettings")["Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(_configuration.GetSection("EmailSettings")["EmailName"], _configuration.GetSection("EmailSettings")["EmailPassword"])
            };

            using (var message = new MailMessage(_configuration.GetSection("EmailSettings")["EmailName"], email)
            {
                Subject = subject,
                Body = htmlMessage
            })
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
