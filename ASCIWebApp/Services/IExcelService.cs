using System;
using System.Collections.Generic;

namespace ASCIWebApp.Services
{
    public interface  IExcelService
    {
        List<string> GetDataFromExcel(string filePath, string uniquecolumn);
    }
}
