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
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    if (worksheet == null)
                    {

                    }
                    else
                    {
                        var rowcount = worksheet.Dimension.Rows;

                        for (int row = 2; row < rowcount; row++)
                        {
                            users.Add(new IACSShort
                            {
                                SocCardNum = (worksheet.Cells[row, 1].Value ?? string.Empty).ToString(),
                                PassportNum = (worksheet.Cells[row, 2].Value ?? string.Empty).ToString(),
                                LAccountNumber = (worksheet.Cells[row, 3].Value ?? string.Empty).ToString(),
                                ANTPType = (worksheet.Cells[row, 4].Value ?? string.Empty).ToString()
                            });
                        }
                    }
                }
            }
            return users;
        }
    }
}
