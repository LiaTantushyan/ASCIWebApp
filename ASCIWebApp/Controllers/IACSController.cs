using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ASCIWebApp.Controllers
{
    public class IACSController : Controller
    {
        private readonly IIACSService _iacsService;
        public IACSController(IIACSService iacsService)
        {
            _iacsService = iacsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadDataFromXml([FromForm] IFormFile xml, [FromForm] IFormFile xlsx)
        {
            if (Path.GetExtension(xml.FileName) != ".xml")
            {
                TempData["Message"] = "Please upload Xml file.TRY AGAIN!!!";
                return RedirectToAction("Index", "IACS");
            }
            if (Path.GetExtension(xlsx.FileName) != ".xlsx")
            {
                TempData["Message"] = "Please upload Xlsx file.TRY AGAIN!!!";
                return RedirectToAction("Index", "IACS");
            }
            if (xml is null || xml.Length == default)
            {
                TempData["Message"] = "Xml file is null or empty";
                return RedirectToAction("Index", "IACS");
            }
            if (xlsx is null || xlsx.Length == default)
            {
                TempData["Message"] = "Xlsx file is null or empty";
                return RedirectToAction("Index", "IACS");
            }
            var data = await _iacsService.GetUsersFromFileAsync(xml);

            return Json(new
            {
                Message = "File was deserialized",
                Succedeed = true,
                Value = data
            });
        }
    }
}
