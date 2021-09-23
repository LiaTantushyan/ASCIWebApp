using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

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
                return Json(new
                {
                    Message = "Please upload Xml file!",
                    Succedeed = false
                });
            }
            if (Path.GetExtension(xlsx.FileName) != ".xlsx")
            {
                return Json(new
                {
                    Message = "Please upload Xlsx file!",
                    Succedeed = false
                });
            }
            if (xml is null || xml.Length == default)
            {
                return Json(new
                {
                    Message = "Xml file is null or empty",
                    Succedeed = false
                });
            }
            if (xlsx is null || xlsx.Length == default)
            {
                return Json(new
                {
                    Message = "Xlsx file is null or empty",
                    Succedeed = false
                });
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
