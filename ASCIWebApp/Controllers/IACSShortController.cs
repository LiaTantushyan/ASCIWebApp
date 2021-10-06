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
using System.Data;
using ClosedXML.Excel;
using System.Configuration;
using System.Data.SqlClient;

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
        public async Task<IActionResult> UploadFileToServer([FromForm]IFormFile xml, [FromForm] IFormFile excel)
        {
            string xmlFileExtension = Path.GetExtension(xml.FileName);
            string excelFileExtension = Path.GetExtension(excel.FileName);


            if (xml.Length == 0 || xml == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Index");
            }

            if (xmlFileExtension != ".xml" || xmlFileExtension != ".txt")
            {
                TempData["message"] = $"XML file must have .xml/.txt extension";
                return View("Index");
            }
            
            if (excel.Length == 0 || excel == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Index");
            }

            if (excelFileExtension != ".xlsx" || excelFileExtension != ".xls")
            {
                TempData["message"] = $"Excel file must have .xlsx/.xls extension";
                return View("Index");
            }

            TempData["message"] = $"Files are succesfully validated. Press Compare to compare them";
            return RedirectToAction("Compare");
        }

        [HttpGet]
        public async Task<IActionResult> Compare()
        {
            return View();
        }

        [HttpPost]
        public async Task<List<IACSShort>> Compare([FromForm] IFormFile xml, [FromForm] IFormFile excel, string uniqueColumn)
        {
            
            var dataFromXml = _xmlService.GetDataFromXml(xml, uniqueColumn);
            var dataFromExcel = _excelService.GetDataFromExcel(excel, uniqueColumn);

            var datadeference = dataFromExcel.Except(dataFromXml);

            DataTable dt = new DataTable("Deferences");
            dt.Columns.Add(uniqueColumn);

            foreach (var item in datadeference)
            {
                dt.Rows.Add(item);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);                
                }
            }
            return default;
        }    
    }
}
