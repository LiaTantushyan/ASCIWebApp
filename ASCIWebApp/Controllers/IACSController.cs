using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ASCIWebApp.Services;
using ASCIWebApp.Helpers;
using ASCIWebApp.Models;

namespace ASCIWebApp.Controllers
{
    public class IACSController : Controller
    {
        private readonly IXmlService _xmlService;
        private readonly IExcelService _excelService;
        private readonly IWebHostEnvironment _webhost;
        public IACSController(IWebHostEnvironment webhost,IXmlService xmlService)
        {
            _webhost = webhost;
            _xmlService = xmlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> UploadFileToServer( IFormFile xmlfile)
        {
            var a = _webhost.WebRootPath;
            var b = xmlfile.FileName;
            var saveXml = Path.Combine(_webhost.WebRootPath, "xmlfile", xmlfile.FileName);
            string fileExt = Path.GetExtension(xmlfile.FileName);
            if (fileExt == ".xml" || fileExt == ".txt")
            {
                using (var uploadXml = new FileStream(saveXml, FileMode.Create))
                {
                    await xmlfile.CopyToAsync(uploadXml);
                    ViewData["message"] = $"the file {xmlfile.FileName} is uploaded";

                }
            }
            else
            {
                ViewData["message"] = $" something gone wrong with {xmlfile.FileName} ";

            }
            var users = _xmlService.GetDataFromXmlAsync(xmlfile);
            return View();
        }
    }
}
