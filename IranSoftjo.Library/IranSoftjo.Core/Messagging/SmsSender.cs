using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mehr.Messagging
{
    public class SmsSender : TemplatedMessageBuilder
    {
        public SmsSender()
        {
            TemplatesFolder = "Templates/SMS/";
            LayoutTemplateFileName = null;
        }

        public void Send(string cellNumber)
        {
            string message = base.BuildMessage();
            File.WriteAllText(@"D:\Work\Active Projects\sepanta\Sepanta CRM\TestChargeOutput\SendedChargeTo." + cellNumber + ".txt", message);
        }
    }
}
