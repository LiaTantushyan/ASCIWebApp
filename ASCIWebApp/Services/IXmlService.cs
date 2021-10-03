using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASCIWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace ASCIWebApp
{
	public interface IXmlService
	{
		List<IACSShort> GetDataFromXml(IFormFile file);
	}
}