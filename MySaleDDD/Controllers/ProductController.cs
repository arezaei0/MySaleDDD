using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySaleDDD.Core.Models;
using MySaleDDD.Data.Repository;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IGenericRepository<Unit> _repoUnit;
        private readonly IGenericRepository<Brand> _repoBrand;
        private IMapper _mapper;
        public ProductController(IGenericRepository<Product> repo,
            IGenericRepository<Unit> repoUnit,
            IGenericRepository<Brand> repoBrand,
            IMapper mapper)
        {
            _repo = repo;
            _repoUnit = repoUnit;
            _repoBrand = repoBrand;
            _mapper = mapper;
        }
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }
        public IActionResult Index()
        {
            IQueryable<Product> list = _repo.GetASQueryable("Unit,Brand");
            IEnumerable<ProductViewModel> result = _mapper.Map<IEnumerable<Product>,
                IEnumerable<ProductViewModel>>(list.ToList().OrderByDescending(x => x.Id));
            return View(result);
        }
        public IActionResult Create()
        {
            FillDropDown();
            return View(new ProductViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product item = _mapper.Map<ProductViewModel, Product>(model);
                
            }
            return View(model);
        }
        private void FillDropDown()
        {
            ViewBag.Units = _repoUnit.GetAll().Select(w => new SelectListItem { Text = w.Titel, Value = w.Id.ToString() });
            ViewBag.Brands = _repoBrand.GetAll().Select(w => new SelectListItem { Text = w.Titel, Value = w.Id.ToString() });
        }
    }
}
