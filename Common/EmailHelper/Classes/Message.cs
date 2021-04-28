using MimeKit;
using MimeKit.Text;
using System.Collections.Generic;
using System.Linq;

namespace Common.EmailHelper.Classes
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public List<MailboxAddress> CC { get; set; }
        public List<MailboxAddress> BCC { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public TextFormat EmailFormat { get; set; }
        public Dictionary<string, byte[]> Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IEnumerable<string> cc = null, IEnumerable<string> bcc = null, TextFormat emailFormat = TextFormat.Html, Dictionary<string, byte[]> attachments = null)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(_ => new MailboxAddress(_)));

            if(cc != null)
            {
                CC = new List<MailboxAddress>();
                CC.AddRange(cc.Select(_ => new MailboxAddress(_)));
            }
            if(bcc != null)
            {
                BCC = new List<MailboxAddress>();
                BCC.AddRange(bcc.Select(_ => new MailboxAddress(_)));
            }
            if(attachments != null)
            {
                Attachments = new Dictionary<string, byte[]>();
                attachments.ToList().ForEach(_ => Attachments.Add(_.Key, _.Value));
            }
            EmailFormat = emailFormat;
            Subject = subject;
            Content = content;
        }
    }
}
