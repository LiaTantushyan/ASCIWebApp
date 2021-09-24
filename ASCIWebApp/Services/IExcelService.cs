using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Services
{
    interface IExcelService
    {
        Task<List<ExcelService>> GetDataFromExcelAsync(IFormFile file);
        
    }
}
