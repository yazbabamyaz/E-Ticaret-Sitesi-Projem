using AutoMapper;
using BusinessLayer.Abstract;
using E_Shop.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Shop.Controllers
{
    public class Home2Controller : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public Home2Controller(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        //[Authorize(Roles ="Admin,User")]//2 yetkili de girebiliyor.
        public IActionResult Index(int page=1,int category=0)
        {
            //TempData["kullanici"]= HttpContext.Session.GetString("kullanici");            
            var products = _productService.GetCategory(category);
            int pagesize = 3;
            var product=_productService.GetCategory(category).Skip((page-1)*pagesize).Take(pagesize).ToList();
            ViewBag.PageCount = (int)Math.Ceiling(products.Count / (double)pagesize);
            ViewBag.PageSize = pagesize;
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentPage = page;

            Product_Category_Model model = new Product_Category_Model
            {
                
                Products = product,
                Categories = _categoryService.GetAll().ToList()
            };

            return View(model);
        }
        
    }
}
