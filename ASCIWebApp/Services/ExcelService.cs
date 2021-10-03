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

namespace ASCIWebApp.Services
{
    public class ExcelService : IExcelService
    {
        public List<IACSShort> GetDataFromExcel(string fileName)
        {
            List<IACSShort> users = new List<IACSShort>();
            var fName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\"}" + "\\" + fileName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = new FileStream(fName, FileMode.Open, FileAccess.Read))
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var columninfo = Enumerable.Range(1, worksheet.Dimension.Columns).ToList().Select(n =>
                       new { Index = n, ColumName = worksheet.Cells[1, n].Value.ToString() }
                    );


                    for (int row = 2; row < worksheet.Dimension.Rows; row++)
                    {
                        //T obj = (T)Activator.CreateInstance(typeof(T));
                        var obj = new IACSShort();
                        foreach (var prop in typeof(IACSShort).GetProperties())
                        {
                            int col = columninfo.SingleOrDefault(c => c.ColumName == prop.Name).Index;
                            var val = worksheet.Cells[row, col].Value;
                            //var propertytype = prop.GetType();
                            prop.SetValue(obj, val);
                        }
                        users.Add(obj);
                    }
                }
            }
            return users;
        }
    }
}
