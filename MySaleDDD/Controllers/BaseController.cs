using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    [Authorize]
    public class BaseController : Controller // اگر همین بیس کنترلر اتورایز کنیم به همه کنترلرها اعمال خواهد شد 
    {
        public  UserManager<ApplicationUser> _userManager;
        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }

        public string UserId { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)  //هر اکشنی بخواهد اتفاق بیفتد میایید اینجاو یوزر ای دی را پر می کنیم
        {
            UserId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            base.OnActionExecuting(context);
        }

    }
}
