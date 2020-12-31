using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MySaleDDD.Core;
using MySaleDDD.Core.Models;
using MySaleDDD.Data.Repository;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class BrandController : Controller
    {
        //private readonly IBrandRepository _repo;   //Dont Use Generic Repository
        private readonly IGenericRepository<Brand> _repo;
        private readonly IMapper _mapper;
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public BrandController(IGenericRepository<Brand> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await _repo.GetAllAysnc();
            IEnumerable<BaseViewModel> brandViewModels = _mapper.Map<IEnumerable<Brand>, IEnumerable<BaseViewModel>>(brands);
            return View(brandViewModels);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new BaseViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = _mapper.Map<BaseViewModel, Brand>(model);
               var result= await _repo.InsertAsync(brand);
                if (result == -1)
                    ErrorMessage = Resources.Messages.Error;
                SuccessMessage = Resources.Messages.Sucess;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Brand brand = await _repo.GetByIdAsync(Id);
            if (brand == null) return NotFound();
            BaseViewModel model = _mapper.Map<Brand, BaseViewModel>(brand);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Brand brand = _mapper.Map<BaseViewModel, Brand>(model);
                var result= await  _repo.UpdateAsync(brand);
                if (result == -1)
                    ErrorMessage = "خطا در ویرایش اطلاعات";
                SuccessMessage = "اطلاعات با موفقیت ویرایش شد";
                return RedirectToAction("Edit",new { Id=model.Id});
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var result= await _repo.RemoveAsync(Id);
            if (result <1)
                ErrorMessage = "خطا در حذف اطلاعات";
            SuccessMessage = "اطلاعات با موفقیت حذف شد";
            return RedirectToAction("Index");
        }
    }
}
