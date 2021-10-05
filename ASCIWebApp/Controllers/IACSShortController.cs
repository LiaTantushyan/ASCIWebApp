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
    public class IACSShortController : Controller
    {
        private readonly IXmlService _xmlService;
        private readonly IExcelService _excelService;
        private readonly IWebHostEnvironment _webhost;
        public IACSShortController(IWebHostEnvironment webhost, IXmlService xmlService, IExcelService excelService)
        {
            _webhost = webhost;
            _xmlService = xmlService;
            _excelService = excelService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileToServer(IFormFile xmlfile, IFormFile xlsxfile)
        {
            string s = Request.Form["aa"].ToString();
            var saveXml = Path.Combine(_webhost.WebRootPath, "xmlfile", xmlfile.FileName);
            var saveXlsx = Path.Combine(_webhost.WebRootPath, "xlsxfile", xlsxfile.FileName);

            string fileExtensionXml = Path.GetExtension(xmlfile.FileName);
            string fileExtensionXlsx = Path.GetExtension(xlsxfile.FileName);


            if (xmlfile.Length == 0 || xmlfile == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Index");
            }

            if (fileExtensionXml == ".xml" || fileExtensionXml == ".txt" )
            {
                using (var uploadXml = new FileStream(saveXml, FileMode.Create))
                {
                    await xmlfile.CopyToAsync(uploadXml);
                }               
            }
            else
            {
                TempData["message"] = $"File must have .xml/.txt extension";
                return View("Index");
            }
            if (xlsxfile.Length == 0 || xlsxfile == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Index");
            }
            if (fileExtensionXlsx == ".xlsx" || fileExtensionXlsx == ".xls")
            {
                using (var uploadXml = new FileStream(saveXml, FileMode.Create))
                {
                    await xmlfile.CopyToAsync(uploadXml);
                    TempData["message"] = $"Files are uploaded";
                }
            }
            else
            {                  
                TempData["message"] = $"File must have .xlsx/.xls extension";
                return View("Index");
            }
            var listXml = _xmlService.GetDataFromXml(xmlfile);
            var listExcel = _excelService.GetDataFromExcel(xlsxfile.FileName);
            await Compare(listXml, listExcel);
            return View("Index");
        }

        public async Task<List<IACSShort>> Compare(List<IACSShort> listXml, List<IACSShort> listXlsx)
        {

            var listDeferences = listXlsx.Except(listXml);
     
            return listDeferences.ToList();
        }
    }
}
