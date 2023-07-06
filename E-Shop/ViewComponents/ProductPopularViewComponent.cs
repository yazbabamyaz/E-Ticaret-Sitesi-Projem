using BusinessLayer.Abstract;
using E_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Shop.ViewComponents
{
    public class ProductPopularViewComponent:ViewComponent
    {
        IProductService _productService;

        public ProductPopularViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public ViewViewComponentResult Invoke()
        {

            var model = new ProductListViewModel
            {
                Products = _productService.GetPopular().Take(3).ToList()

            };
            return View(model);
        }
    }
}
