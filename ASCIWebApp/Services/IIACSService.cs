using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASCIWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace ASCIWebApp
{
	public interface IIACSService
	{
		Task<List<IACS>> GetUsersFromFileAsync(IFormFile file);

		Task ReportXmlToDatabase(List<IACS> data);
	}
}