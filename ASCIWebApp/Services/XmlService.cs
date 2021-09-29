using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var path= GetFilePath(file);
            List<string> datalist=new List<string>();
            XElement root = XElement.Load(path);

            //urn:cba-am:ed:v1.0
            XNamespace ed = "urn:cba-am:ed:v1.0";

            var reports = (from report in root.Descendants("IACSBankCustomer")
                            where report.Element("IACS").Value.Contains("BankCustomer")
                           select new IACSShort
                           {
                               PassportNum = report.Element("PassportNum").Value,
                               SocCardNum = report.Element("SocCardNum").Value,
                               LAccountNumber = report.Element("LAccountNumber").Value,
                               ANTPType = report.Element("ANTPType").Value
                           }).ToList();

            foreach (var item in reports)
            {
               
            }
            return datalist;
        }
    }
   
}

