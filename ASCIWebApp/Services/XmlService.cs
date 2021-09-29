using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public string GetFilePath(IFormFile formFile)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                formFile.CopyToAsync(stream);
            }
            return filePath;
        }

        public List<IACSShortModel> GetDataFromXml(IFormFile file)
        {
            var path = GetFilePath(file);

            XElement root = XElement.Load(path);
            XNamespace ed = "urn:cba-am:ed:v1.0";

            var passportNumbers = root.Descendants(ed + "PassportNum").ToArray();
            var soccardNumbers = root.Descendants(ed + "SocCardNum").ToArray();
            var accountNumbers = root.Descendants(ed + "LAccountNumber").ToArray();
            var antpTypes = root.Descendants(ed + "ANTPType").ToArray();

            var result = new IACSShortModel[passportNumbers.Length];

            for (int i = 0; i < passportNumbers.Length; i++)
            {
                result[i] = new IACSShortModel
                {
                    PassportNum = passportNumbers[i].Value,
                    SocCardNum = soccardNumbers[i].Value,
                    LAccountNumber = accountNumbers[i].Value,
                    ANTPType = antpTypes != null ? antpTypes[i].Value.ToString() : string.Empty
                };
            }

            return result.ToList();
        }
    }
}