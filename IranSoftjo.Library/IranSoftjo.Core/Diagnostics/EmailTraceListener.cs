using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;

namespace Mehr.Diagnostics
{

    public class EmailTraceListener : TraceListener
    {
        ITraceEmailer _traceEmailer;

        #region overrides
        public override void Write(string message)
        {
            CurrentTraceEmailer.SendTraceEmail(message);
        }

        public override void WriteLine(string message)
        {
            CurrentTraceEmailer.SendTraceEmail(message);
        }

        protected override string[] GetSupportedAttributes()
        {
            return new string[] { "fromAddress", "toAddress", "subject" };
        }
        #endregion

        #region properties
        public ITraceEmailer CurrentTraceEmailer
        {
            get
            {
                if (_traceEmailer == null)
                {
                    _traceEmailer = new TraceEmailer(
                        Attributes["fromAddress"],
                        Attributes["toAddress"],
                        Attributes["subject"]);
                }

                return _traceEmailer;
            }
        }
        #endregion
    }

    public interface ITraceEmailer
    {
        void SendTraceEmail(string message);

        string MailFrom { get; set; }
        string MailTo { get; set; }
        string Subject { get; set; }
    }
    public class TraceEmailer : ITraceEmailer
    {
        public TraceEmailer(string from, string to, string subject)
        {
            MailFrom = from;
            MailTo = to;
            Subject = subject;
        }

        public void SendTraceEmail(string message)
        {
            using (MailMessage msg = new MailMessage())
            {
                msg.From = new MailAddress(MailFrom);
                foreach (string s in MailTo.Split(";".ToCharArray()))
                {
                    msg.To.Add(s);
                }
                msg.Subject = Subject;

                SmtpClient smtp = new SmtpClient();
                smtp.Send(msg);
            }
        }

        #region properties
        private string _mailFrom;
        public string MailFrom
        {
            get
            {
                return _mailFrom;
            }
            set
            {
                _mailFrom = value;
            }
        }

        private string _mailTo;
        public string MailTo
        {
            get
            {
                return _mailTo;
            }
            set
            {
                _mailTo = value;
            }
        }

        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }
        #endregion
    }

}
