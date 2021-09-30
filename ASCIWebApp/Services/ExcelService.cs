using ASCIWebApp.Models;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Services
{
    public class ExcelService : IExcelService
    {
        public string GetFilePath(IFormFile formFile)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(filePath))
            {
                formFile.CopyToAsync(stream);
            }
            return filePath;
        }
        Task<List<IACSShortModel>> IExcelService.DataExcel(IFormFile file)
        {
            var filePath = GetFilePath(file);
            filePath = System.IO.Path.GetTempFileName().Replace(".tmp", ".xlsx");
            //Taken List of data from json file which we want to export to excel.
            List<IACSShortModel> stulist = new List<IACSShortModel>();
            DataTable dt = new DataTable();
            //required  using ClosedXML.Excel;        
            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath)) //refer previous code to get the file path.
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);
                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }

                }
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!(row[0] is DBNull))
                    {
                        IACSShortModel student = new IACSShortModel();
                        student.SocCardNum = row[0].ToString();
                        student.PassportNum = row[1].ToString();
                        student.LAccountNumber = row[2].ToString();
                        student.ANTPType = row[3].ToString();
                        stulist.Add(student);
                    }
                }
            }
            return null;
        }
    }
}
