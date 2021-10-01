﻿using ASCIWebApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASCIWebApp.Services
{
    public interface  IExcelService
    {
        List<IACSShort> GetDataFromExcel(string fileName);
    }
}
