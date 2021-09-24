using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Services
{
    public class ExcelService : IExcelService
    {
        Task<List<ExcelService>> IExcelService.GetDataFromExcelAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
