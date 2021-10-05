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

namespace ASCIWebApp.Services
{
    public class ExcelService : IExcelService
    {
        public List<string> GetDataFromExcel(IFormFile file, string uniqueColumn)
        {
            var filePath = XmlCustomSerializer.GetFilePath(file);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<string> result = new List<string>();

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
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
