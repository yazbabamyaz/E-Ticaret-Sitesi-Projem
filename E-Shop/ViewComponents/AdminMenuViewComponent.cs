using DataAccessLayer.Context;
using E_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace E_Shop.ViewComponents
{
    public class AdminMenuViewComponent:ViewComponent
    {
        private AppDbContext _context;

        public AdminMenuViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public ViewViewComponentResult Invoke()
        {
            var count=_context.Products.Where(x=>x.Stock<50).Count();
            ViewBag.count=count;
            var count2 = _context.Products.Where(x => x.Stock == 50).Count();
            ViewBag.count2 = count2;          
            return View();
        }
    }
}
