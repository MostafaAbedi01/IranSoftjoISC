using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public class StringMan
    {
        public const char Char1 = '{';
        public const char Char2 = ':';
        public const char Char3 = ':';
        public const char Char4 = '}';

        public static string Format(string template, IDictionary<string, string> plcaeholderValues)
        {
            StringBuilder message = new StringBuilder(template.Length + plcaeholderValues.Values.Sum(v => v.Length));
            int startIndex = -1;
            for (int i = 0; i < template.Length; i++)
            {
                var c = template[i];
                if (c == Char1 &&
                    template[i + 1] == Char2 &&
                    template[i + 2] == Char3)
                {
                    if (startIndex != -1)
                        message.Append(template, startIndex, i - startIndex);
                    startIndex = i;
                    i += 2;
                }
                else if (startIndex != -1)
                {
                    if (c == Char4)
                    {
                        string key = template.Substring(startIndex + 3, i - startIndex - 3);
                        string value;
                        if (plcaeholderValues.TryGetValue(key, out value))
                            message.Append(value);
                        else
                            message.Append(template, startIndex, i - startIndex + 1);
                        startIndex = -1;
                    }
                }
                else
                {
                    message.Append(c);
                }
            }
            if (startIndex != -1)
                message.Append(template, startIndex, (template.Length - 1) - startIndex);
            return message.ToString();
        }
    }
}
