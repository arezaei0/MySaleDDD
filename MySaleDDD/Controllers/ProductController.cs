using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySaleDDD.Core.Models;
using MySaleDDD.Data.Repository;
using MySaleDDD.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MySaleDDD.Controllers
{
    public class ProductController : BaseController
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
        public IActionResult Index()
        {
            IQueryable<Product> list = _repo.GetASQueryable("Unit,Brand");
            IEnumerable<ProductViewModel> result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(list.ToList().OrderByDescending(x => x.Id));
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            FillDropDown();
            return View(new ProductViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product item = _mapper.Map<ProductViewModel, Product>(model);
                if (model.MyFile != null)
                {
                    using(var ms=new MemoryStream())
                    {
                        model.MyFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        item.ProductImage = fileBytes;
                    }
                }
                await _repo.InsertAsync(item);
                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;
                return RedirectToAction("Edit", new { Id = item.Id });
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            FillDropDown();
            Product model = await _repo.GetByIdAsync(Id);
            ProductViewModel result = _mapper.Map<Product, ProductViewModel>(model);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product item = _mapper.Map<ProductViewModel,Product>(model);
                var file = Request.Form.Files;
                if (file.Count > 0)
                {
                    byte[] p = null;
                    using (var fs=file[0].OpenReadStream())
                    {
                        using(var ms=new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            p = ms.ToArray();
                            item.ProductImage = p;
                        }
                    }

                }

                await _repo.UpdateAsync(item);
                SuccessMessage = Resources.Messages.ChangesSavedSuccessfully;
                return RedirectToAction("Edit",new { Id= item.Id});
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _repo.DeleteAsync(Id);
            if (result < 1)
            {
                if (TempData["Error"] != null)
                {
                    TempData.Remove("Error");
                }
                TempData.Add("Error", Resources.Messages.ProblemDeletingItem);
            }
            return RedirectToAction("Index");
        }
        private void FillDropDown()
        {
            ViewBag.Units = _repoUnit.GetAll().Select(w => new SelectListItem { Text = w.Titel, Value = w.Id.ToString() });
            ViewBag.Brands = _repoBrand.GetAll().Select(w => new SelectListItem { Text = w.Titel, Value = w.Id.ToString() });
        }
    }
}
