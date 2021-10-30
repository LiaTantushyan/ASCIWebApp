using System;
using System.Collections.Generic;

namespace ASCIWebApp
{
	public interface IXmlService
	{
		List<string> GetDataFromXml(string uri, string uniquecolumn);
	}
}