﻿using Microsoft.AspNetCore.Mvc;
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
            var saveXml = Path.Combine(_webhost.WebRootPath, "xmlfile", xmlfile.FileName);
            string fileExt = Path.GetExtension(xmlfile.FileName);
            if (xmlfile.Length == 0 || xmlfile == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View();
            }
            if (fileExt == ".xml" || fileExt == ".txt")
            {
                using (var uploadXml = new FileStream(saveXml, FileMode.Create))
                {
                    await xmlfile.CopyToAsync(uploadXml);
                    TempData["message"] = $"the file {xmlfile.FileName} is uploaded";
                }
            }
            else
            {
                TempData["message"] = $"File must have .xml/.txt extension";
                return View();
            }
           // var users = _xmlService.GetDataFromXml(xmlfile);
            return RedirectToAction("Index","Excel");
        }
    }
}
    