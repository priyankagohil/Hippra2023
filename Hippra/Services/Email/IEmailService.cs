using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hippra.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task FTSendAdminEmailAsync(string email, string subject, string message);

    }
}
