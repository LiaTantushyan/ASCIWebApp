using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Services
{
    interface IExcelInfoService
    {
        Task<List<ExcelInfoService>> GetDataFromExcelAsync(IFormFile file);
        
    }
}
