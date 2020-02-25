using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FTEmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync( 
            string email, string subject, string message);
        Task FTSendEmailAsync(string senderName, string senderEmail,
            string email, string subject, string message);
        Task FTSendAdminEmailAsync(string email, string subject, string message);
    }
}
