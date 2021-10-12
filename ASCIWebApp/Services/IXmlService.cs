using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ASCIWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace ASCIWebApp
{
	public interface IXmlService
	{
		//List<string> GetDataFromXml(string file, string uniqueColumn);

		IEnumerable<string> GetDataFromXml(string uri, string uniquecolumn);
	}
}