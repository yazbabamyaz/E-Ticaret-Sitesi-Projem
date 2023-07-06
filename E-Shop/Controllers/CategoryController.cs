using AutoMapper;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    public class CategoryController : Controller
    {
        IProductService _productService;
        private readonly IMapper _mapper;

        public CategoryController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Details(int id) 
        {
            var products = _productService.GetCategory(id);
            //var model=_mapper.Map<ProductListViewModel>(products);
            return View(products);
        }
    }
}
