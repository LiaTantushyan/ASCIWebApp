using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ASCIWebApp.Services;
using System.Collections.Generic;
using ASCIWebApp.Models;
using ASCIWebApp.Helpers;

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
        
        //public IActionResult Index(List<IACSShort> users=null)
        //{
        //    users = users == null ? new List<IACSShort>(): users;
        //    return View(users);
        //}
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileExcelToServer(IFormFile xlsxfile)
        {
            if(xlsxfile.Length==0 || xlsxfile == null)
            {
                TempData["message"] = "Something is wrong with file, TRY AGAIN!";
                return View();
            }
            var saveXml = Path.Combine(_webhost.WebRootPath, xlsxfile.FileName);
            string fileExtension = Path.GetExtension(xlsxfile.FileName);
            if (fileExtension == ".xlsx" || fileExtension=="xls")
            {
                if (System.IO.File.Exists(xlsxfile.ContentDisposition)){
                    System.IO.File.Delete(xlsxfile.ContentDisposition);
                }
                using (var fileStream = new FileStream(saveXml, FileMode.Create))
                {
                    await xlsxfile.CopyToAsync(fileStream);
                    TempData["message"] = "Xlsx file is uploaded";
                }
            }
            else
            {
                TempData["message"] = "Wrong format file/Null file, please try again";
                return View();
            }
            var users = _excelService.GetDataFromExcel(xlsxfile.FileName);
            return View();
        }

    }

}