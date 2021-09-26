using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ASCIWebApp.Data;
using ASCIWebApp.Helpers;
using ASCIWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace ASCIWebApp.Services
{
    public class XmlService : IXmlService
    {
        private readonly IACSDbContext _db;
        public XmlService(IACSDbContext db)
        {
            _db = db;
        }

        public async Task<string> GetDataFromXmlAsync(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            var tempFile = File.Create(filePath);

            using (tempFile = new FileStream(filePath, FileMode.Append))
            {
                await file.CopyToAsync(tempFile);

                XElement root = XElement.Load(filePath);
                XNamespace ed = "urn:cba-am:ed:v1.0";
                IEnumerable<string> textSegs =
                            from seg in root.Descendants(ed + "SecondName")
                            select (string)seg;

                string str = textSegs.Aggregate(new StringBuilder(),
                    (sb, i) => sb.Append(i),
                    sp => sp.ToString()
                );

                return str;
            }

            return string.Empty;
        }
    }
}
