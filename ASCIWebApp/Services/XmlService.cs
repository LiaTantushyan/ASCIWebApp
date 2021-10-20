using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ASCIWebApp.Data;

namespace ASCIWebApp.Services
{
    public class XmlService : IXmlService
    {
        private readonly IACSDbContext _db;

        public XmlService(IACSDbContext db)
        {
            _db = db;
        }
    
        public IEnumerable<string> GetDataFromXml(string uri, string uniquecolumn)
        {
            using (XmlReader reader = XmlReader.Create(new StreamReader(uri,Encoding.UTF8)))
            {
                XElement name = null;
                reader.MoveToContent();
               
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name.Equals("ed:" + uniquecolumn))
                        {
                            {
                                name = XElement.ReadFrom(reader) as XElement;

                                yield return name.Value;
                            }
                        }
                    }
                }
            }
        }
    }
}