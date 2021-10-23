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
        private static string UniqueColumn;
        private static List<string> Difference { get; set; }
        private static string XmlPath;
        private static string ExcelPath;

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
        public async Task<IActionResult> CompareUploadedFiles(IFormFile xml, IFormFile excel)
        {
            var xmlResult = ThrowMessageIfInvalid(xml, FileTypes.Xml, new string[] { ".xml", ".txt" });
            if (!string.IsNullOrEmpty(xmlResult))
            {
                TempData["Message"] = xmlResult;
                return View("Index");
            }

            XmlPath = XmlCustomSerializer.GetFilePath(xml);

            var excelResult = ThrowMessageIfInvalid(excel, FileTypes.Excel, new string[] { ".xlsx", ".xls" });
            if (!string.IsNullOrEmpty(excelResult))
            {
                TempData["Message"] = excelResult;
                return View("Index");
            }

            ExcelPath =  XmlCustomSerializer.GetFilePath(excel);

            return RedirectToAction("Compare");
        }

        [HttpGet]
        public IActionResult Compare()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetDatasDifference()
        {
            UniqueColumn = Request.Form["selectvalue"].ToString();

            var dataFromXml = _xmlService.GetDataFromXml(XmlPath, UniqueColumn).ToList();
            var dataFromExcel = _excelService.GetDataFromExcel(ExcelPath, UniqueColumn);

            if (dataFromXml != null && dataFromExcel != null)
            {
                Difference = dataFromExcel.Except(dataFromXml).ToList();
               
                if (Difference != null)
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
                var worksheet = package.Workbook.Worksheets.Add("Differences");
                worksheet.DefaultColWidth=18;
                worksheet.Cells["A1"].Value = UniqueColumn;
                worksheet.Cells["A2"].LoadFromCollection(Difference);
                package.Save();
            }

            stream.Position = 0;
            return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Differences.xlsx");
        }

        private string ThrowMessageIfInvalid(IFormFile file,FileTypes type, string[] extensions)
        {
            if (file.Length == default || file is null)
            {
                return $"Your {type} file is null or empty";
            }

            if (!extensions.Contains(Path.GetExtension(file.FileName)))
            {
                return $"Your {type} extension is invalid";
            }

            return string.Empty;
        }
    }
}
