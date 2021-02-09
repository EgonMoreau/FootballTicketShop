using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace webproject1920.Util.Mail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailOrderAsync(string email, string subject, string message, string[] orderLines);
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(
            string email, string subject, string message)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(_emailSettings.Sender);
            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient(_emailSettings.MailServer))
                {
                    smtp.Port = _emailSettings.MailPort;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        public async Task SendEmailOrderAsync(
            string email, string subject, string message, string[] orderLines)
        {
            var mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(_emailSettings.Sender);
            mail.Subject = subject;

            foreach(string oL in orderLines)
            {
                message += "<hr />" + oL + "<hr />";
            }

            mail.Body = message;
            mail.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient(_emailSettings.MailServer))
                {
                    smtp.Port = _emailSettings.MailPort;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
