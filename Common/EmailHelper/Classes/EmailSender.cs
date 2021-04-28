using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Linq;


namespace Common.EmailHelper.Classes
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public bool SendEmail(Message message)
        {
            Send(CreateMessage(message));
            return true;
        }
        private MimeMessage CreateMessage(Message message)
        {
            BodyBuilder bodyBuilder = new BodyBuilder
            {
                TextBody = message.EmailFormat == MimeKit.Text.TextFormat.Text ? message.Content : string.Empty,
                HtmlBody = message.EmailFormat == MimeKit.Text.TextFormat.Html ? message.Content : string.Empty
            };

            if (message.Attachments?.Count > 0)
                message.Attachments.ToList().ForEach(_ => bodyBuilder.Attachments.Add(_.Key, _.Value));

            MimeMessage emailMessage = new MimeMessage
            {
                Subject = message.Subject,
                Body = bodyBuilder.ToMessageBody()
            };

            emailMessage.From.Add(new MailboxAddress(_emailSettings.From));
            emailMessage.To.AddRange(message.To);
            if (message.CC?.Count > 0)
                emailMessage.Cc.AddRange(message.CC);
            if (message.BCC?.Count > 0)
                emailMessage.Bcc.AddRange(message.BCC);

            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port);
                if (client.AuthenticationMechanisms.Count > 0)
                    client.Authenticate(_emailSettings.Username, _emailSettings.Password);

                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
