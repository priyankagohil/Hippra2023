using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static FTEmailService.EmailService;

namespace Hippra.Services.Email
{
    public class SendgridEmailService : IEmailService
    {
        //  private readonly IOptions<PostMarkOptions> _postmarkSettings;
        private readonly SendGridClient _sendgridClient;
        public SendgridEmailService(string apiKey)
        {
            _sendgridClient = new SendGridClient(apiKey);
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {

            if (null == email)
            {
                return;
            }
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(email))
            {
                return;
            }


            var fromEmail = "support@hippra.com";

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, "Hipra"),
                Subject = subject,
                //TemplateId = "d-e0a128f366f348a18a0282db292739d1",
                HtmlContent = message

            };

            msg.AddTo(new EmailAddress(email, email));


            try
            {
                var response = await _sendgridClient.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var t = e;
            }
        }

        public async Task FTSendAdminEmailAsync(string email, string subject, string message)
        {

            if (null == email)
            {
                return;
            }
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(email))
            {
                return;
            }

            var fromEmail = "support@hippra.com";

            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, "Hipra"),
                Subject = subject,
                //TemplateId = "d-e0a128f366f348a18a0282db292739d1",
                HtmlContent = "Messaghe from: " + email + " Message:" + message

            };

            msg.AddTo(new EmailAddress(fromEmail, fromEmail));


            try
            {
                var response = await _sendgridClient.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var t = e;
            }

        }
    }
}



