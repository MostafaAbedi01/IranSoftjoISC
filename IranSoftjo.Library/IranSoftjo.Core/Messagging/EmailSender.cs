using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Mehr.Messagging
{
    public class EmailSender : TemplatedMessageBuilder
    {
        public EmailSender()
        {
            TemplatesFolder = "Templates/Email/";
            LayoutTemplateFileName = "Layout.html";
            ReservedPlaceHolderValues.Add("title", Subject);
        }

        string subject;
        public string Subject
        {
            get { return subject; }
            set
            {
                if (ReservedPlaceHolderValues["title"] == subject)
                    ReservedPlaceHolderValues["title"] = value;
                subject = value;
            }
        }

        public void Send(string receiver)
        {
            Send(null, receiver);
        }

        public void Send(string subject, string receiver)
        {
            MailMessage mailMessage = new MailMessage()
            {
                Body = BuildMessage(),
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
                Subject = subject ?? Subject,
            };
            mailMessage.To.Add(receiver);
            SmtpClient client = new SmtpClient();
            // client.SendCompleted +=
            client.SendAsync(mailMessage, null);
        }
    }
}
