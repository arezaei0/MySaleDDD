using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySaleDDD.Core.Models;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, UserManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous] // یعنی بدون اوتورایز همه دسترسی داشته باشند
        public async Task<IActionResult> Login(string returnUrl=null)
        {
            if (User.Identity.IsAuthenticated) //در کوکی هست که طرف چند دقیقه پیش لاگین کرده است
            {

                ViewData["ReturnUrl"] = returnUrl;
               return RedirectToAction("Index","Home");
            }
            else
            {
                await HttpContext.SignOutAsync(); // کابر رو می اندزیم بیرون
            }
            ViewData["RerurnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded)
                {
                   return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
