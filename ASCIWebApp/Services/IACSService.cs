using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ASCIWebApp.Data;
using ASCIWebApp.Helpers;
using ASCIWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace ASCIWebApp.Services
{
    public class IACSService : IIACSService
    {
        private readonly IACSDbContext _db;
        public IACSService(IACSDbContext db)
        {
            _db = db;
        }

        public async Task<List<IACS>> GetDataFromFileAsync(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            var tempFile = File.Create(filePath);
            await file.CopyToAsync(tempFile);
          
            XDocument xtemp = new XDocument(tempFile);

            XNamespace ed = "urn:cba-am:ed:v1.0";
            IEnumerable<string> textSegs =from seg
             in xtemp.Descendants(ed + "SocCardNum" + "PassportNum" + "LAccountNumber" + "ANTPType")
            select (string)seg;

            string str = textSegs.Aggregate(new StringBuilder(),
                (sb, i) => sb.Append(i),
                sp => sp.ToString()
            );
            tempFile.Dispose();
            return textSegs as List<IACS>;
        }

        public async Task ReportXmlToDatabase(List<IACS> data)
        {
            foreach (var item in data)
            {
                await _db.AddAsync(item);
            }

            await _db.SaveChangesAsync();
        }
    }
}
