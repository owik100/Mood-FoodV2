using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska.Infrastructure
{
   public interface IMyEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
