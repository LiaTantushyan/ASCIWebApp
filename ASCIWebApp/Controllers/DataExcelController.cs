using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using ASCIWebApp.Services;
using ASCIWebApp.Helpers;
using System.Collections.Generic;
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
      
        public DataExcelController(IXmlService xmlService, IExcelService excelService)
        {
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
            var xmlResult = ThrowMessageIfInvalid(xml, FileTypes.Xml, new string[] { ".xml", ".txt" });
            if (!string.IsNullOrEmpty(xmlResult))
            {
                TempData["Message"] = xmlResult;
                return View("Index");
            }

            xmlpath = XmlCustomSerializer.GetFilePath(xml);

            var excelResult = ThrowMessageIfInvalid(excel, FileTypes.Excel, new string[] { ".xlsx", ".xls" });
            if (!string.IsNullOrEmpty(excelResult))
            {
                TempData["Message"] = excelResult;
                return View("Index");
            }

            excelpath =  XmlCustomSerializer.GetFilePath(excel);

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
            var stream = new MemoryStream();
           
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Deferences");
                worksheet.DefaultColWidth=18;
                worksheet.Cells["A1"].Value = selectedfield;
                worksheet.Cells["A2"].LoadFromCollection(datadeference);
                package.Save();
            }

            stream.Position = 0;
            return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Deference.xlsx");
        }

        private string ThrowMessageIfInvalid(IFormFile file,FileTypes type, string[] extensions)
        {
            if (file.Length == default || file is null)
            {
                return $"Your {type.ToString()} file is null or empty";
            }

            if (!extensions.Contains(Path.GetExtension(file.FileName)))
            {
                return $"Your {type.ToString()} extension is invalid";
            }

            return string.Empty;
        }
    }
}
