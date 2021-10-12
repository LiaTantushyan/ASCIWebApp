using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ASCIWebApp.Data;
using ASCIWebApp.Helpers;
using ASCIWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASCIWebApp.Services
{
    public class XmlService : IXmlService
    {
        private readonly IACSDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public XmlService(IACSDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IEnumerable<string> StreamCustomerItem(string uri, string uniquecolumn) {
            using (XmlReader reader = XmlReader.Create(uri))
            {
                XElement name = null;
                XElement item = null;     
                reader.MoveToContent();

                // Parse the file, save header information when encountered, and yield the
                // Item XElement objects as they're created.
                // Loop through Customer elements.
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "BankCustomer")
                    {
                        // move to Name element
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element &&
                                reader.Name == uniquecolumn)
                            {
                                name = XElement.ReadFrom(reader) as XElement;
                                
                                yield return name.Value;                               
                            }
                        }

                        //// loop through Item elements
                        //while (reader.Read())
                        //{
                        //    if (reader.NodeType == XmlNodeType.EndElement)
                        //        break;
                        //    if (reader.NodeType == XmlNodeType.Element
                        //        && reader.Name == "Item")
                        //    {
                        //        item = XElement.ReadFrom(reader) as XElement;
                        //        if (item != null)
                        //        {
                        //            XElement tempRoot = new XElement("Root",
                        //                new XElement(name)
                        //            );
                        //            tempRoot.Add(item);
                        //            yield return item;
                        //        }
                        //    }
                        //}
                    }
                }
            }
        }
    }
}