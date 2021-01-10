using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySaleDDD.Core.Models;
using MySaleDDD.Helper;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class UserController : BaseController
    {
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var model = _userManager.Users.Select(x => new UserViewModel  //نحوه مپ کردن مانند قبلی ها تیست چون از ریپازیتوری نمی گیریم و از یوزرمنیجر می گیریم  
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Mobile = x.Mobile,
                UserName = x.UserName,
                Id = x.Id
            });
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //کلیمز هایی رو که نوشتیم رو لیستش(لیست فرم ها) رو می دیم به ویوبگ زیر
            ViewBag.Claims = typeof(AccessClaims).GetFields().Select(x => new KeyValueViewModel
            {
                Key =
                x.Name,
                Value = x.GetValue(new AccessClaims()).ToString()
            }).ToList(); //name=key , value=Display Read From AccessClaims Class
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (model.Password == null)
                return View(model);
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Mobile = model.Mobile,
                    AddDate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, model.Password); //Create User

                //Save Roles and Claimes 
                if (result.Succeeded)
                {
                    if (await _roleManager.FindByNameAsync(model.RoleType.ToString()) == null)
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = model.RoleType.ToString().ToUpper(),
                            NormalizedName = model.RoleType.ToString().ToUpper()  //User.IsInRole("Admin") ادمین بزرگ یا کوچک حساس نمی شود
                        });
                    }
                    await _userManager.AddToRoleAsync(user, model.RoleType.ToString()); // نقش کاربر را تخصیص می دهیم
                    if (model.Claims.Count > 0) //اگر فرمی تخصیص داده شده است?
                    {
                        var claims = model.Claims.Select(x => new Claim(nameof(AccessClaims), x));// نام کلیمز رو اختصاص میده و نه دیسپلیی که برایش تعیین کردیم را
                        await _userManager.AddClaimsAsync(user, claims);
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            ViewBag.Claims = typeof(AccessClaims).GetFields().Select(x => new KeyValueViewModel
            {
                Key = x.Name,
                Value = x.GetValue(new AccessClaims()).ToString()
            }).ToList();
            var item = await _userManager.FindByIdAsync(Id);
            if (item != null)
            {
                var model = _mapper.Map<ApplicationUser, UserViewModel>(item);
                var roles = await _userManager.GetRolesAsync(item);
                if (roles.Any()) //اگر رولی وجود داشت
                    model.RoleType = Enum.Parse<RoleType>(roles.FirstOrDefault()); // نوع رول رو پر می کنیم
                //فرم هایی که دسترسی داشته رو می کشیم بیرون و در لیست استرینگ ها قرار بدیم و پاس بدیم بره 
                // باید از کلیمز ها بخوانیم
                model.Claims = (await _userManager.GetClaimsAsync(item)).Where(x => x.Type == nameof(AccessClaims)).Select(x => x.Value).ToList();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (model.Password == null)
                return View(model);
            if (ModelState.IsValid)
            {
                var userFounded = await _userManager.FindByIdAsync(model.Id);
                if (userFounded != null)
                {
                    userFounded.UserName = model.UserName;
                    userFounded.FirstName = model.FirstName;
                    userFounded.LastName = model.LastName;
                    userFounded.Email = model.Email;
                    userFounded.PhoneNumber = model.PhoneNumber;
                    userFounded.EmailConfirmed = true;
                    userFounded.PhoneNumberConfirmed = true;
                    userFounded.Mobile = model.Mobile;
                    userFounded.LastModified = DateTime.Now;
                };

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(userFounded);
                    await _userManager.ResetPasswordAsync(userFounded, token, model.Password);
                }
                await _userManager.UpdateAsync(userFounded);
                //می خواهیم رول های کابر را اپدیت کنیم
                var user = await _userManager.FindByIdAsync(model.Id);  //چون اپدیت شده است از اول پیدا می کنیم
                var roles = await _userManager.GetRolesAsync(user);
                // اول رول های قبلی را پاک می کنیم
                await _userManager.RemoveFromRolesAsync(user, roles);
                if (await _roleManager.FindByNameAsync(model.RoleType.ToString()) == null)
                {
                    await _roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = model.RoleType.ToString().ToUpper(),
                        NormalizedName = model.RoleType.ToString().ToUpper()  //User.IsInRole("Admin") ادمین بزرگ یا کوچک حساس نمی شود
                    });
                }
                //رول های جدید را می زنیم
                await _userManager.AddToRoleAsync(user, model.RoleType.ToString());
                await _userManager.RemoveClaimsAsync(user, (await _userManager.GetClaimsAsync(user)));

                if (model.Claims.Count > 0) //اگر فرمی تخصیص داده شده است?
                {
                    var claims = model.Claims.Select(x => new Claim(nameof(AccessClaims), x));// نام کلیمز رو اختصاص میده و نه دیسپلیی که برایش تعیین کردیم را
                    await _userManager.AddClaimsAsync(user, claims);
                }
                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;
                return RedirectToAction("Edit", new { Id = userFounded.Id });
            }
            ErrorMessage = "اطلاعات کاربر جاری نامعتبر است";
            return View(model);
        }
    }
}
