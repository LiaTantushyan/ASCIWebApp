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

namespace ASCIWebApp.Controllers
{
    public class IACSShortController : Controller
    {
        public static string xmlpath;
        public static string excelpath;

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
        public async Task<IActionResult> UploadFileToServer(IFormFile xml, IFormFile excel)
        {
            xmlpath = XmlCustomSerializer.GetFilePath(xml);
            excelpath = XmlCustomSerializer.GetFilePath(excel);

            string xmlFileExtension = Path.GetExtension(xml.FileName);
            string excelFileExtension = Path.GetExtension(excel.FileName);


            if (xml.Length == 0 || xml == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Alert");
            }

            if (xmlFileExtension != ".xml" || xmlFileExtension != ".txt")
            {
                TempData["message"] = $"XML file must have .xml/.txt extension";
                //return View("Index");
            }

            if (excel.Length == 0 || excel == null)
            {
                TempData["message"] = "Please upload a file that is not null or empty";
                return View("Alert");
            }

            if (excelFileExtension != ".xlsx" || excelFileExtension != ".xls")
            {
                TempData["message"] = $"Excel file must have .xlsx/.xls extension";
                //  return View("Index");
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
            string selectedfield = Request.Form["selectvalue"].ToString();

            var dataFromXml = _xmlService.GetDataFromXml(xmlpath, selectedfield).ToList();
            var dataFromExcel = _excelService.GetDataFromExcel(excelpath, selectedfield).ToList();

            if (dataFromXml != null && dataFromExcel != null)
            {
                var datadeference = dataFromExcel.Except(dataFromXml).ToList();
                if (datadeference != null)
                {
                    CreateExcelFile(selectedfield, datadeference);
                        TempData["message"] = "Excel files is created";
                        return View("Alert");               
                }
                else
                {
                    TempData["message"] = "There are not any deference value ";
                    return View("Alert");
                }

            }
            TempData["message"] = "List of xml or excel file is null";
            return View("Alert");
        }
        public FileResult CreateExcelFile(string selectedcolumn,List<string> datadeference)
        {
            //NorthwindEntities entities = new NorthwindEntities();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[1] { new DataColumn(selectedcolumn)});

            foreach (var customer in datadeference)
            {
                dt.Rows.Add(customer);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
}


