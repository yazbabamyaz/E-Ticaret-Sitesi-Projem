using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Shop.ViewComponents
{
    public class SliderListViewComponent:ViewComponent
    {
        IProductService _productService;

        public SliderListViewComponent(IProductService productService)
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
