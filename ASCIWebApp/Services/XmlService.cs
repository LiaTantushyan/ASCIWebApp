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

        public List<string> GetDataFromXml(IFormFile file, string uniqueColumn)
        {
            XElement root = XElement.Load(XmlCustomSerializer.GetFilePath(file));
            XNamespace ed = "urn:cba-am:ed:v1.0";

            var listSelectedField = root.Descendants(ed + uniqueColumn)
                .Select(i=>i.Value.ToString())
                .ToList();
           
            return listSelectedField;
        }
    }
}