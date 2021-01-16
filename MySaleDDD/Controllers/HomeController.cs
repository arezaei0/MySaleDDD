using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySaleDDD.Core.Models;
using MySaleDDD.Data.Repository;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IGenericRepository<Order> _repoOrder;
        private readonly IMapper _mapper;
        public HomeController(IGenericRepository<Product> repo, IMapper mapper, IGenericRepository<Order> repoOrder)
        {
            _repo = repo;
            _mapper = mapper;
            _repoOrder = repoOrder;
        }

        public IActionResult Index()
        {
            IQueryable<Product> list = _repo.GetASQueryable("Unit,Brand");
            IEnumerable<ProductViewModel> result = _mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(list);
            return View(result);
        }

        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel()
            { RequestId = "خطا در احراز هویت" });
        }

        [HttpPost(Name ="Card")] //معمولا ایجکسی ها را نام می دهیم
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Card(Tag tag)
        {
            var listCard = await _repoOrder.GetAllAysnc();
            if(tag.bootstapClassname.Equals("btn btn-danger"))
            {
                Order remove = listCard.Where(x => x.ProductId == tag.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                if (remove == null)
                    return Json(new { success = false, responseText = "محصولی در سبد خرید شما نیست" });
                await _repoOrder.RemoveAsync(remove.Id);
                return Json(new { success = false, responseText = "محصولی جاری از سبد شما حذف شد" }); //Success false یعنی اینکه حذف می کنیم و موقع افزودن تروو می کنیم
            }
            Product product = await _repo.GetByIdAsync(tag.Id);
            int customerOrder = listCard.Where(woak => woak.ProductId == tag.Id).Count();
            if (customerOrder > product.Qty)
                return Json(new { success = false, responseText = "محصول موجود نیست" });
            Order order = new Order
            {
                ProductId = tag.Id,
                Confirm = true,
                Titel = product.Titel + " " + product.ProductCode + " " + product.Size,
                SystemUserId = UserId  //فعلا دستی دادیم بعد پر می کنیم
            };
            var result = await _repoOrder.InsertAsync(order);
            return Json(new { success = true, responseText = "محصول در سبد شما ثبت شد" });
        }
    }
}
