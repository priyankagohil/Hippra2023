using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
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
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credential =
                        new System.Net.NetworkCredential(mailUser, mailUserPwd);
                    client.EnableSsl = true;
                    client.Credentials = credential;

                    MailMessage msg = new MailMessage(new MailAddress(_senderEmail, _senderName), new MailAddress(email, ""));
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    // client.Send(msg);
                    await client.SendMailAsync(msg);
                    client.Dispose();

                }
                catch
                {
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
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
               
                    client.EnableSsl = true;
                    System.Net.NetworkCredential credential =
                   new System.Net.NetworkCredential(mailUser, mailUserPwd);
                    client.Credentials = credential;

                    MailMessage msg = new MailMessage(new MailAddress(_senderEmail, _senderName), new MailAddress(email, ""));
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    // client.Send(msg);
                    await client.SendMailAsync(msg);
                    client.Dispose();


                }
                catch (Exception e)
                {
                    var t = e;
                    return;
                }
            }
            return;
        }



        public async Task FTSend2AdminEmailAsync(string remail, string subject, string message)
        {

            if (null == remail)
            {
                return;
            }
            RegexUtilities util = new RegexUtilities();
            if (!util.IsValidEmail(remail))
            {
                return;
            }

            if (null != _emailAccount && null != _emailCred)
            {
                string mailUser = _emailAccount;
                string mailUserPwd = _emailCred;
                try
                {
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credential =
                        new System.Net.NetworkCredential(mailUser, mailUserPwd);
                    client.EnableSsl = true;
                    client.Credentials = credential;

                    MailMessage msg = new MailMessage(new MailAddress(_senderEmail, _senderName), new MailAddress(_senderEmail, ""));
                    msg.Subject = remail + " : " + subject;
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    // client.Send(msg);
                    await client.SendMailAsync(msg);
                    client.Dispose();


                }
                catch // (SmtpException e)
                {
                    // Console.WriteLine("Hi!");
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

            if (null != _emailAccount && null != _emailCred)
            {
                string mailUser = _emailAccount;
                string mailUserPwd = _emailCred;
                try
                {

                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credential =
                        new System.Net.NetworkCredential(mailUser, mailUserPwd);
                    client.EnableSsl = true;
                    client.Credentials = credential;

                    MailMessage msg = new MailMessage(new MailAddress(senderEmail, senderName), new MailAddress(email, ""));
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    //client.Send(msg);
                    await client.SendMailAsync(msg);
                    
                    client.Dispose();

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
