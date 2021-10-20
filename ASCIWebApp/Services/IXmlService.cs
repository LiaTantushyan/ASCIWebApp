using System;
using System.Collections.Generic;

namespace ASCIWebApp
{
	public interface IXmlService
	{
		IEnumerable<string> GetDataFromXml(string uri, string uniquecolumn);
	}
}