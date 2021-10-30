using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ASCIWebApp.Services
{
    public class XmlService : IXmlService
    {
        public List<string> GetDataFromXml(string uri, string uniquecolumn)
        {
            var result = new List<string>();
            using (XmlReader reader = XmlReader.Create(new StreamReader(uri, Encoding.UTF8)))
            {
                XElement name = null;
                try
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name.Equals("ed:" + uniquecolumn))
                            {
                                name = XElement.ReadFrom(reader) as XElement;
                                result.Add(name.Value);
                            }
                        }
                    }
                }

                catch (Exception)
                {
                    return new List<string>();
                }

                return result;
            }
        }
    }
}