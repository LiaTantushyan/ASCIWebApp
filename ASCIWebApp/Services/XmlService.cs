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

            var saveXml = Path.Combine("http://localhost:33178/", "file", file.FileName);

            using (var uploadXml = new FileStream(saveXml, FileMode.Create))
            {
                 await file.CopyToAsync(uploadXml);   
            }
            return string.Empty;
        }
    }
}

