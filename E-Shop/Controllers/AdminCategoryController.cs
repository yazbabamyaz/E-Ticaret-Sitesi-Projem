using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using E_Shop.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Shop.Controllers
{
    public class AdminCategoryController : Controller
    {
        ICategoryService _categoryService;

    
        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
           
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            CategoryListViewModel model= new CategoryListViewModel
            {
                Categories = categories
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [ValidateAntiForgeryToken]//güvenlik için-url den isteği engelliyor-
        [HttpPost]        
        public IActionResult Create(Category category)
        {

            //isvalid hata verdi category sayfasında products var o yüzden.
            //burayı düzeltirsin validasyon vs
            try
            {
                _categoryService.Add(category);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Bir Hata Oluştu");
                return View();
            }
              
        }
        

        [HttpPost]
        public IActionResult Delete(int id)
        {
            //önce db de bu id ye ait kayıt var mı diye kontrol edebilirdik ajax ile
            _categoryService.Delete(id);

            return Json(true);
        }


    public IActionResult Update(int id) 
        { 
            var updatedValue = _categoryService.CategoryGetById(id);
            return View(updatedValue); 
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Update(Category category)
        {
            try
            {
                var updatedValue = _categoryService.CategoryGetById(category.Id);
                updatedValue.Name = category.Name;
                updatedValue.Description = category.Description;
                _categoryService.Update(updatedValue);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Bir Hata Oluştu");
                return View();
            }
            
        }
    }
}
