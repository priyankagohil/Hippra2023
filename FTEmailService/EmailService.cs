using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FTEmailService
{
    public class EmailService : IEmailSender
    {
        private string _emailAccount;
        private string _emailCred;
        private string _senderName;
        private string _senderEmail;

        public EmailService(
            string emailAccount,
            string emailCred,
            string senderName,
            string senderEmail)
        {
            _emailAccount = emailAccount;
            _emailCred = emailCred;
            _senderName = senderName;
            _senderEmail = senderEmail;

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

            if (null != _emailAccount && null != _emailCred)
            {
                string mailUser = _emailAccount;
                string mailUserPwd = _emailCred;
                try
                {

                    // create message using MailKit
                    var msg = new MimeMessage();
                    msg.From.Add(new MailboxAddress(_senderName, _senderEmail));
                    msg.To.Add(new MailboxAddress("", email));
                    msg.Subject = subject;

                    msg.Body = new TextPart("html")
                    {
                        Text = message
                    };

                    // send

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp-mail.outlook.com", 587, false);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(mailUser, mailUserPwd);

                        client.Send(msg);
                        client.Disconnect(true);
                    }

                }
                catch (Exception e)
                {
                    var text = e;
                    return;
                }
            }
            return;
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

            if (null != _emailAccount && null != _emailCred)
            {
                string mailUser = _emailAccount;
                string mailUserPwd = _emailCred;
                try
                {

                    // create message using MailKit
                    var msg = new MimeMessage();
                    msg.From.Add(new MailboxAddress(_senderName, _senderEmail));
                    msg.To.Add(new MailboxAddress("", _senderEmail));
                    msg.Subject = subject;

                    msg.Body = new TextPart("html")
                    {
                        Text = message
                    };

                    // send

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp-mail.outlook.com", 587, false);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(mailUser, mailUserPwd);

                        client.Send(msg);
                        client.Disconnect(true);
                    }

                }
                catch
                {
                    return;
                }
            }
            return;
        }


        public async Task FTSendEmailAsync(string senderName, string senderEmail, 
            string email, string subject, string message)
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
            
            if (null != _emailAccount && null!= _emailCred)
            {
                string mailUser = _emailAccount;
                string mailUserPwd = _emailCred;
                try
                {

                    // create message using MailKit
                    var msg = new MimeMessage();
                    msg.From.Add(new MailboxAddress(senderName, senderEmail));
                    msg.To.Add(new MailboxAddress("", email));
                    msg.Subject = subject;

                    msg.Body = new TextPart("html")
                    {
                        Text = message
                    };

                    // send

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.Connect("smtp-mail.outlook.com", 587, false);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        // Note: only needed if the SMTP server requires authentication
                        client.Authenticate(mailUser, mailUserPwd);

                        client.Send(msg);
                        client.Disconnect(true);
                    }

                }
                catch
                {
                    return;
                }
            }
            return;
        }

        public class RegexUtilities
        {
            bool invalid = false;

            public bool IsValidEmail(string strIn)
            {
                invalid = false;
                if (String.IsNullOrEmpty(strIn))
                    return false;

                // Use IdnMapping class to convert Unicode domain names.
                try
                {
                    strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                          RegexOptions.None, TimeSpan.FromMilliseconds(200));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }

                if (invalid)
                    return false;

                // Return true if strIn is in valid e-mail format.
                try
                {
                    return Regex.IsMatch(strIn,
                          @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                          RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                }
                catch (RegexMatchTimeoutException)
                {
                    return false;
                }
            }

            private string DomainMapper(Match match)
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            }
        }
    }

}
