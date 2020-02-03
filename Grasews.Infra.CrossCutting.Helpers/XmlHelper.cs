using System;
using System.Text;

namespace Grasews.Infra.CrossCutting.Helpers
{
    public static class XmlHelper
    {
        public static string EscapeXml(this string s)
        {
            var toXml = s;

            if (!string.IsNullOrEmpty(toXml))
            {
                // replace literal values with entities
                toXml = toXml.Replace("&", "&amp;");
                toXml = toXml.Replace("'", "&apos;");
                toXml = toXml.Replace("\"", "&quot;");
                toXml = toXml.Replace(">", "&gt;");
                toXml = toXml.Replace("<", "&lt;");
            }

            return toXml;
        }

        public static string UnescapeXml(this string s)
        {
            var unXml = s;

            if (!string.IsNullOrEmpty(unXml))
            {
                // replace entities with literal values
                unXml = unXml.Replace("&apos;", "'");
                unXml = unXml.Replace("&quot;", "\"");
                unXml = unXml.Replace("&gt;", ">");
                unXml = unXml.Replace("&lt;", "<");
                unXml = unXml.Replace("&amp;", "&");
            }

            return unXml;
        }

        public static string XmlString(string text, bool isAttribute = false)
        {
            var sb = new StringBuilder(text.Length);

            foreach (var chr in text)
            {
                if (chr == '<')
                    sb.Append("&lt;");
                else if (chr == '>')
                    sb.Append("&gt;");
                else if (chr == '&')
                    sb.Append("&amp;");

                // special handling for quotes
                else if (isAttribute && chr == '\"')
                    sb.Append("&quot;");
                else if (isAttribute && chr == '\'')
                    sb.Append("&apos;");

                // Legal sub-chr32 characters
                else if (chr == '\n')
                    sb.Append(isAttribute ? "&#xA;" : "\n");
                else if (chr == '\r')
                    sb.Append(isAttribute ? "&#xD;" : "\r");
                else if (chr == '\t')
                    sb.Append(isAttribute ? "&#x9;" : "\t");
                else
                {
                    if (chr < 32)
                        throw new InvalidOperationException($"Invalid character in Xml String. Chr {Convert.ToInt16(chr)} is illegal.");
                    sb.Append(chr);
                }
            }

            return sb.ToString();
        }
    }
}