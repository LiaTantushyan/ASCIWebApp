using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ExcelController.Controllers
{
    public class IACSController : Controller
    {
        private readonly IWebHostEnvironment _webhost;
        public IACSController(IWebHostEnvironment webhost)
        {
            _webhost = webhost;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFileExcelToServer(IFormFile xlsxfile)
        {
            if (xlsxfile == null || xlsxfile.Length == 0)
                return Content("File Not Selected");

            string fileExtension = Path.GetExtension(xlsxfile.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
                ViewData["message"] = $"File not selected";

            var saveXml = Path.Combine(_webhost.WebRootPath, "xlsxfile", xlsxfile.FileName);

            using (var fileStream = new FileStream(saveXml, FileMode.Create))
            {
                await xlsxfile.CopyToAsync(fileStream);
            }

            //if (file.Length <= 0)
            //    return BadRequest(GlobalValidationMessage.FileNotFound);

            //using (ExcelPackage package = new ExcelPackage(fileLocation))
            //{
            //    ExcelWorksheet workSheet = package.Workbook.Worksheets["Table1"];
            //    //var workSheet = package.Workbook.Worksheets.First();
            //    int totalRows = workSheet.Dimension.Rows;

            //    var DataList = new List<Customers>();

            //    for (int i = 2; i <= totalRows; i++)
            //    {
            //        DataList.Add(new Customers
            //        {
            //            CustomerName = workSheet.Cells[i, 1].Value.ToString(),
            //            CustomerEmail = workSheet.Cells[i, 2].Value.ToString(),
            //            CustomerCountry = workSheet.Cells[i, 3].Value.ToString()
            //        });
            //    }

            //    _db.Customers.AddRange(customerList);
            //    _db.SaveChanges();
            //}
            return View();
        }

    }
   
}