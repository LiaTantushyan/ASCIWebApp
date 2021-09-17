using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<IACS>> GetUsersFromFileAsync(IFormFile file)
        {
	        var data = XmlCustomSerializer.DeserializeFromXmlFile<List<IACS>>(file);
	        if (data != null)
	        {
				await ReportXmlToDatabase(data);
			}

	        return data;
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
