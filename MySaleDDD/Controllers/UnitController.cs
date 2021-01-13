using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MySaleDDD.Core.Models;
using MySaleDDD.Data.Repository;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class UnitController : BaseController
    {
        //private readonly IBrandRepository _repo;   //Dont Use Generic Repository
        private readonly IGenericRepository<Unit> _repo;
        private readonly IMapper _mapper;
        public UnitController(IGenericRepository<Unit> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Unit> units = await _repo.GetAllAysnc();
            IEnumerable<BaseViewModel> UnitViewModels = _mapper.Map<IEnumerable<Unit>, IEnumerable<BaseViewModel>>(units);
            return View(UnitViewModels);
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
                Unit unit = _mapper.Map<BaseViewModel, Unit>(model);
                var result = await _repo.InsertAsync(unit);
                if (result == -1)
                    ErrorMessage = Resources.Messages.Error;
                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;
                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Unit unit = await _repo.GetByIdAsync(Id);
            if (unit == null) return NotFound();
            BaseViewModel model = _mapper.Map<Unit, BaseViewModel>(unit);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Unit unit = _mapper.Map<BaseViewModel, Unit>(model);
                var result = await _repo.UpdateAsync(unit);
                if (result == -1)
                    ErrorMessage = "خطا در ویرایش اطلاعات";
                SuccessMessage = "اطلاعات با موفقیت ویرایش شد";
                return RedirectToAction("Edit", new { Id = model.Id });
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _repo.RemoveAsync(Id);
            if (result < 1)
                ErrorMessage = "خطا در حذف اطلاعات";
            SuccessMessage = "اطلاعات با موفقیت حذف شد";
            return RedirectToAction("Index");
        }
    }
}
