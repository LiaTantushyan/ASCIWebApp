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
using Ganss.Excel;

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
                    List<string> ColumnNames = new List<string>();
                    int row = 1;
                    for (int column = 1; column <= worksheet.Dimension.End.Column; column++)
                    {
                        if (worksheet.Cells[row, column].Value.ToString() == uniqueColumn)
                        {                       
                            while (row < worksheet.Dimension.End.Row)
                            {
                                row++;
                                result.Add(worksheet.Cells[row, column].Value.ToString());
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
