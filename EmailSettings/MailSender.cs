using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace michealogundero.EmailSettings
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _config;
        readonly ILogger<MailSender> _logger;
        public MailSender(ILogger<MailSender> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _myEmail = _config.GetValue<string>("MyEmail");
            _myOtherEmail = _config.GetValue<string>("MyOtherEmail");
            _myEmailPassword = _config.GetValue<string>("MyEmailPassword");

        }

        private string _myEmail;
        private string _myOtherEmail;
        private string _myEmailPassword;


        public async Task<string> SendEmail(string name, string message, string subject, string email)
        {
            try
            {
                string FromAddress = _myEmail;
                string FromAdressTitle = "Portfolio Contact Form";
                string[] ToAddress = { _myEmail, _myOtherEmail };
                string Subject = subject;
                string BodyContent = message;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                foreach (var address in ToAddress)
                {
                    mimeMessage.To.Add(new MailboxAddress(address));

                }
                if (Subject is null)
                {
                    Subject = "No Subject";
                }
                mimeMessage.Subject = Subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = BodyContent + "<br/> <h4>This mail was sent from " + email + "(" + name + ")</h4>"
                };

                var senderMimeMessage = new MimeMessage();
                senderMimeMessage.From.Add(new MailboxAddress(_myOtherEmail));
                senderMimeMessage.To.Add(new MailboxAddress(email));
                senderMimeMessage.Subject = "Acknowledgement Mail";
                if (senderMimeMessage.Subject == null)
                {
                    senderMimeMessage.Subject = "Acknowledgement Mail";
                }
                senderMimeMessage.Body = new TextPart("html")
                {
                    Text = "Hi" + name + "," +
                    "Thanks for contacting me. I would get back to you shortly. Micheal Ogundero"
                };

                using (var emailClient = new SmtpClient())
                {

                    try
                    {
                        emailClient.SslProtocols |= SslProtocols.Tls;
                        emailClient.CheckCertificateRevocation = false;
                        await emailClient.ConnectAsync("smtp.gmail.com", 465, false);
                        await emailClient.AuthenticateAsync(_myEmail, _myEmailPassword);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Gmail server failed to authenticate or connect");
                        _logger.LogError("sorry! cannot send mail");

                        throw new Exception("Gmail server failed to authenticate or connect, cannot send mail.");
                    }


                    await emailClient.SendAsync(mimeMessage);

                    await emailClient.DisconnectAsync(true);

                    _logger.LogError("Email Sent successfully");
                }

                return "The mail has been sent successfully !!";
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in sending mail");
                _logger.LogError(ex.Message);
                throw;

            }

        }
    }
}
