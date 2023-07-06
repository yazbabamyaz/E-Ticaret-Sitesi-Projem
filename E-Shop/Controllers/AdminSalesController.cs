using DataAccessLayer.Context;
using E_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Controllers
{
    public class AdminSalesController : Controller
    {
        private AppDbContext _context;

        public AdminSalesController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            
            var model = new SalesViewModel();
            if (User.Identity.IsAuthenticated)
            {
                
                model = new SalesViewModel
                {
                    Sales = _context.Sales.Include(x => x.Product).ToList(),
                };
            }

            return View(model);
        }
    }
}
