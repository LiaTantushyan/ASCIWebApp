using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

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
                    try
                    {
                        for (int column = 1; column <= worksheet.Dimension.End.Column; column++)
                        {
                            int row = 1;
                            if (worksheet.Cells[row, column].Value.ToString().Equals(uniqueColumn, StringComparison.OrdinalIgnoreCase))
                            {
                                while (row < worksheet.Dimension.End.Row)
                                {
                                    row++;
                                    var value = worksheet.Cells[row, column].Value;
                                    if (value != null)
                                    {
                                        result.Add(value.ToString());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return result;
                    }
                }
            }
            return result;
        }
    }
}

