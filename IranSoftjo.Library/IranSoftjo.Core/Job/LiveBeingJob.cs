using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Mehr.Setting;
using System.Net;
using System.IO;

namespace Mehr.Job
{
    public class LiveBeingJob : JobBase
    {
        protected override string DoAction()
        {
            var url = SettingReader.Get(JobName + ".MonitoringUrl", "http://monitoring.software.sepanta.net/");
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(url);
            StreamReader streamReader = new StreamReader(stream);
            var text = streamReader.ReadToEnd();
            if (text.ToUpper() != "OK")
                TraceSource.TraceEvent(TraceEventType.Error, 1999, "{0} : IsDisabled", this.GetType().Name);

            return text;
        }
    }
}
