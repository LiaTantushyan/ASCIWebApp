using ASCIWebApp.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using OfficeOpenXml;
using Xml.Schema.Linq;
using ASCIWebApp.Helpers;
using ClosedXML.Excel;
using System.Web;
using System.Web.Mvc;
using ASCIWebApp.Data;
using Windows.UI.Xaml;

namespace ASCIWebApp.Services
{
    public class ExcelService : IExcelService
    {
        public List<string> GetDataFromExcel(string filePath, string uniqueColumn)
        {
            
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<string> result = new List<string>();
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                {
                    var data = reader.AsDataSet();
                    
                    foreach (DataTable table in data.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            foreach (DataColumn column in table.Columns)
                            {
                                if (column.ColumnName == uniqueColumn)
                                {
                                    result.Add(row[column.ColumnName].ToString());
                                }       
                            }
                        }
                    }
                }
            }
            return result;
        }
     
    }
}

