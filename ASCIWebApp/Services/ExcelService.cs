using ASCIWebApp.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Services
{
    public class ExcelService : IExcelService
    {
        /*Task<List<Excel>> IExcelService.GetDataFromFileAsync(IFormFile file)
        {
            List<Excel> users = new List<Excel>();
            var fileName = file.FileName;
            // For .net core, the next line requires the NuGet package, 
            // System.Text.Encoding.CodePages
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each row of the file
                    {
                        users.Add(new Excel
                        {
                            SocCardNum = reader.GetValue(0).ToString(),
                            PassportNum = reader.GetValue(1).ToString(),
                            LAccountNumber = reader.GetValue(2).ToString()
                        });
                    }
                }
            }
            return <List<Excel>>users;
        }*/
    }
}
