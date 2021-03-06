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
        [RequestFormLimits(MultipartBodyLengthLimit = 5600000000)]
        [RequestSizeLimit(5600000000)]
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

            ExcelPath = XmlCustomSerializer.GetFilePath(excel);

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

            var dataFromXml = _xmlService.GetDataFromXml(XmlPath, UniqueColumn);
            var dataFromExcel = _excelService.GetDataFromExcel(ExcelPath, UniqueColumn);

            if (dataFromXml is null)
            {
                TempData["message"] = "Something is wrong in your Xml file or received list is empty";
                return View("Index");
            }
            if (dataFromExcel.Count != 0)
            {
                Difference = dataFromExcel.Except(dataFromXml).ToList();

                if (Difference.Count != 0)
                {
                    return View("CreateExcel");
                }
                else
                {
                    TempData["message"] = "There are not any deference value";
                    return View("Index");
                }
            }
            TempData["message"] = "Received list from Excel is empty";
            return View("Index");
        }

        [HttpGet]
        public IActionResult CreateExcelFile()
        {
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Differences");
                worksheet.DefaultColWidth = 20;
                worksheet.Cells["A1"].Value = UniqueColumn;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A2"].LoadFromCollection(Difference);
                var row = worksheet.Dimension.End.Row;
                worksheet.Cells[$"A1:A{row}"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                package.Save();
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Differences.xlsx");       
        }

        private string ThrowMessageIfInvalid(IFormFile file, FileTypes type, string[] extensions)
        {
            if (file is null || file.Length == default)
            {
                return $"Upload {type} file!";
            }

            if (!extensions.Contains(Path.GetExtension(file.FileName)))
            {
                return $"Your {type} extension is invalid";
            }

            return string.Empty;
        }
    }
}
