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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

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
        public async Task GetDataFromXmlAsync(IFormFile file)
        {

            var saveXml = Path.Combine(_webHostEnvironment.WebRootPath,file.FileName);

            try
            {
                using (var uploadXml = new FileStream(saveXml, FileMode.Create))
                {
                    await file.CopyToAsync(uploadXml);
                }
            }
            catch(Exception e)
            {
               
            }
            
        }
    }
}

