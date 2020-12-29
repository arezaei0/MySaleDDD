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
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;
        public BrandController(IBrandRepository repo, IMapper mapper)
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
                await _repo.InsertAsync(brand);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
