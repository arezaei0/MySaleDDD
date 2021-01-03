using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class BaseController : Controller // اگر همین بیس کنترلر اتورایز کنیم به همه کنترلرها اعمال خواهد شد 
    {
        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }
    }
}
