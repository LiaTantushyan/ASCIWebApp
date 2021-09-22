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
		public async Task<IActionResult> UploadDataFromXml([FromForm] IFormFile file)
		{
			if (Path.GetExtension(file.FileName) != ".xml")
			{
				 ViewData["Message"] = "Please upload Xml,try Again!";
				return View();
			}

			else if (file is null || file.Length == default)
			{
				return Json(new
				{
					Message = "File is null or empty",
					Succedeed = false
				});
			}

			var data = await _iacsService.GetUsersFromFileAsync(file);

			return Json(new
			{
				Message = "File was deserialized",
				Succedeed = true,
				Value = data
			});
		}
	}
}
