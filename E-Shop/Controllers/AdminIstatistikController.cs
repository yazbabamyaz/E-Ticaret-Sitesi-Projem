using DataAccessLayer.Context;
using E_Shop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminIstatistikController : Controller
    {
        private ICartSessionService _cartSessionService;
        private AppDbContext _context;

        public AdminIstatistikController(AppDbContext context, ICartSessionService cartSessionService)
        {
            _context = context;
            _cartSessionService = cartSessionService;
        }

        public IActionResult Index()
        {
            var sales = _context.Sales.Count();
            ViewBag.sales=sales;

            var product=_context.Products.Count();
            ViewBag.product=product;

            var category = _context.Categories.Count();
            ViewBag.category=category;

            var sepet = _cartSessionService.GetCart();
            ViewBag.sepet=sepet.Sayi;

            var user = _context.Users.Count();
            ViewBag.user = user;
            return View();
        }
    }
}
