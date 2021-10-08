using Microsoft.AspNetCore.Mvc;
using System;
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
using System.Collections.Generic;
using System.Threading;
using ASCIWebApp.Data;
using OfficeOpenXml;

namespace ASCIWebApp.Controllers
{
    public class DataExcelController : Controller
    {
        public static string selectedfield;
        public static List<string> datadeference { get; set; }

        public static string xmlpath;
        public static string excelpath;

        private readonly IXmlService _xmlService;
        private readonly IExcelService _excelService;
        private readonly IWebHostEnvironment _webhost;
        public DataExcelController(IWebHostEnvironment webhost, IXmlService xmlService, IExcelService excelService)
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
        [RequestFormLimits(MultipartBodyLengthLimit = 3355443200)]
        [RequestSizeLimit(3355443200)]
        public async Task<IActionResult> UploadFileToServer(IFormFile xml, IFormFile excel)
        {
            string xmlFileExtension = Path.GetExtension(xml.FileName);
            string excelFileExtension = Path.GetExtension(excel.FileName);

            xmlpath = XmlCustomSerializer.GetFilePath(xml);
            excelpath = XmlCustomSerializer.GetFilePath(excel);

            if (xml.Length == 0 || xml == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Index");
            }

            if (xmlFileExtension != ".xml" && xmlFileExtension != ".txt")
            {
                TempData["message"] = $"XML file must have .xml/.txt extension";
                return View("Index");
            }

            if (excel.Length == 0 || excel == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Index");
            }

            if (excelFileExtension != ".xlsx" && excelFileExtension != ".xls")
            {
                TempData["message"] = $"Excel file must have .xlsx/.xls extension";
                return View("Index");
            }
            return RedirectToAction("Compare");
        }

        [HttpGet]
        public IActionResult Compare()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompareList()
        {
            selectedfield = Request.Form["selectvalue"].ToString();

            var dataFromXml = _xmlService.GetDataFromXml(xmlpath, selectedfield).ToList();
            var dataFromExcel = _excelService.GetDataFromExcel(excelpath, selectedfield).ToList();

            if (dataFromXml != null && dataFromExcel != null)
            {
                 datadeference = dataFromExcel.Except(dataFromXml).ToList();
                if (datadeference != null)
                {                   
                    return View("CreateExcel");
                }
                else
                {
                    TempData["message"] = "There are not any deference value ";
                    return View("Index");
                }

            }
            TempData["message"] = "List of xml or excel file is null";
            return View("Index");
        }
        [HttpGet]
        public IActionResult CreateExcelFile()
        {
            var data = datadeference;
            var stream = new MemoryStream();
           
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Deferences");
                worksheet.DefaultColWidth=18;
                worksheet.Cells["A1"].Value = selectedfield;
                worksheet.Cells["A2"].LoadFromCollection(data);
                package.Save();
            }

            stream.Position = 0;
            return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Deference.xlsx");
        }

    }
}


