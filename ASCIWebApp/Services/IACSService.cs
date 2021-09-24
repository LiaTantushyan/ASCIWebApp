using System;
using System.Collections.Generic;
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
            XNamespace ed = "urn:cba-am:ed:v1.0";
            XElement root = file as XElement;
            IEnumerable<string> textSegs =from seg in root.Descendants(ed + "SocCardNum" + "PassportNum" + "LAccountNumber")
            select (string)seg;

            string str = textSegs.Aggregate(new StringBuilder(),
                (sb, i) => sb.Append(i),
                sp => sp.ToString()
            );
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
