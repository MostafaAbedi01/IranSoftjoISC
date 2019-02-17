using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Mehr.Messagging
{
    public abstract class TemplatedMessageBuilder
    {
        public static readonly IPathResolver DefaultPathResolver = new DefaultPathResolver();

        public IPathResolver PathResolver { get; set; }
        public string TemplatesFolder { get; set; }
        public string LayoutTemplateFileName { get; set; }
        public string ContentTemplateFileName { get; set; }

        public TemplatedMessageBuilder()
        {
            PathResolver = DefaultPathResolver;
        }

        Dictionary<string, string> dynamicReservedPlaceHolderValues = new Dictionary<string, string>();
        public Dictionary<string, string> DynamicReservedPlaceHolderValues
        {
            get { return dynamicReservedPlaceHolderValues; }
            set { dynamicReservedPlaceHolderValues = new Dictionary<string, string>(value); }
        }

        Dictionary<string, string> reservedPlaceHolderValues = new Dictionary<string, string>();
        public Dictionary<string, string> ReservedPlaceHolderValues
        {
            get { return reservedPlaceHolderValues; }
            set { reservedPlaceHolderValues = new Dictionary<string, string>(value); }
        }

        string finalTemplate;
        protected string BuildMessage()
        {
            if (string.IsNullOrEmpty(finalTemplate))
            {
                finalTemplate = GetBaseTemplate();
                foreach (var item in ReservedPlaceHolderValues)
                    finalTemplate = finalTemplate.Replace(string.Concat("{::", item.Key, "}"), item.Value);
            }
            string message = finalTemplate;
            foreach (var item in DynamicReservedPlaceHolderValues)
                message = message.Replace(string.Concat("{::", item.Key, "}"), item.Value);
            return message;
        }

        private string GetBaseTemplate()
        {
            string baseTemplate = "{::content}";
            if (!string.IsNullOrEmpty(LayoutTemplateFileName))
                baseTemplate = ReadTemplateFile(LayoutTemplateFileName);

            string contentTemplate = ReadTemplateFile(ContentTemplateFileName);
            baseTemplate = baseTemplate.Replace("{::content}", contentTemplate);

            return baseTemplate;
        }

        private string ReadTemplateFile(string fileName)
        {
            string filePath = PathResolver.Resolve(TemplatesFolder + fileName);
            return File.ReadAllText(filePath);
        }
    }

}
