using E_Shop.Models;
using E_Shop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Shop.ViewComponents
{
    public class CartSummaryViewComponent:ViewComponent
    {

        //Sepet bilgisi görüntüleyeceksek sepet session a ulaşmak lazım
        ICartSessionService _cartSessionService;

        public CartSummaryViewComponent(ICartSessionService cartSessionService)
        {
            _cartSessionService = cartSessionService;
        }
        public ViewViewComponentResult Invoke()
        {
            TempData["kullanici"]= HttpContext.Session.GetString("kullanici");
            //bir model gönderelim.
            var model = new CartSummaryViewModel
            {
                Cart = _cartSessionService.GetCart()
            };
            return View(model);
        }

    }
}
