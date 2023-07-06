using BusinessLayer.Abstract;
using E_Shop.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Shop.ViewComponents
{
    public class CategoryListViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        private IProductService _productService;

        public CategoryListViewComponent(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public ViewViewComponentResult Invoke()
        {            

            var model = new CategoryListViewModel
            {

            Categories = _categoryService.GetAll(),
            products=_productService.GetAll()            
                
            };
            return View(model); 
        }  
    }
}
