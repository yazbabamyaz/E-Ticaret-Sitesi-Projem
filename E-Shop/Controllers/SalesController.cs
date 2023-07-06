using DataAccessLayer.Context;
using E_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model=new SalesViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciId = HttpContext.Session.GetString("userid");
                 model = new SalesViewModel
                {
                    Sales = _context.Sales.Include(x => x.Product).Where(x => x.UserId ==Convert.ToInt32(kullaniciId)).ToList(),
                };
            }
           

            
            return View(model);
        }
    }
}
