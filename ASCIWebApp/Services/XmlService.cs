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
        public List<string> GetDataFromXmlAsync(IFormFile file)
        {
            var path = GetFilePath(file);
            List<string> datalist = new List<string>();
            XElement root = XElement.Load(path);

            //urn:cba-am:ed:v1.0
            XNamespace ed = "urn:cba-am:ed:v1.0";

            IEnumerable<IACSShort> a = (from job in root.Descendants(ed + "BankCustomer")
                                        select new IACSShort
                                        {
                                            PassportNum = (string)job.Element(ed + "PassportNum").Value,
                                            SocCardNum = (string)job.Element(ed + "SocCardNum").Value,
                                            LAccountNumber = (string)job.Element(ed + "LAccountNumber").Value,
                                            ANTPType = (string)job.Element(ed + "ANTPType").Value
                                        });
            foreach (var item in a)
            {
                Console.WriteLine(item.PassportNum);
            }
            return datalist;
        }
    }

}

