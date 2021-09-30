using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ASCIWebApp.Services;

namespace ExcelController.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IWebHostEnvironment _webhost;
        private readonly IExcelService _excelService;

        public ExcelController(IWebHostEnvironment webhost, IExcelService excelService)
        {
            _webhost = webhost;
            _excelService = excelService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileExcelToServer(IFormFile xlsxfile)
        {
            if (xlsxfile == null)
                return Content("File Not Selected");
            var saveXml = Path.Combine(_webhost.WebRootPath, xlsxfile.FileName);
            string fileExtension = Path.GetExtension(xlsxfile.FileName);
            if (fileExtension == ".xlsx")
            {
                using (var fileStream = new FileStream(saveXml, FileMode.Create))
                {
                    await xlsxfile.CopyToAsync(fileStream);
                }
            }
            var users = _excelService.DataExcel(xlsxfile);
            return View("UploadFileExcelToServer");
        }

    }

}