using Microsoft.AspNetCore.Identity;
using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MySaleDDD.Helper
{
    public class AccessClaims
    {
        public readonly string AccessToBaseInfo = "اطلاعات پایه";
        public readonly string AccessToUsers = "کاربران";
        public readonly string AccesToProducts = "محصولات";
        public readonly string AccessToReport = "گزارش";
    }

    public static class Utility
    {
        public static async Task<bool> HasAccess(this ClaimsPrincipal User, UserManager<ApplicationUser> userManager, string access)
        {
            if (User.IsInRole("Admin")) //اگر ادمین بود دسترسی کامل دارد
                return true;
            var user = await userManager.FindByNameAsync(User.Identity.Name); // یوزر رو پیدا می کنیم
            var claims = await userManager.GetClaimsAsync(user); // کلیمز ها رو از دیتابیس در میاریم که چقدر فرم داریم. یوز رو می دهیم تا کلیمزهاش رو به ما نشون بده
            return claims.Any(x => x.Value == access); // اگر پیدا کندTrue می شود
        }
    }



}
