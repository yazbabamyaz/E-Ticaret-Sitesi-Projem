using DataAccessLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Comment()
        {
            //include ile entityleri birbirine bağlıyoruz.
            return View(_context.Comments.Include(x => x.User).Include(x=>x.Product).ToList());
        }
        public IActionResult Delete(int id) 
        {
            var delete=_context.Comments.Where(x=>x.Id==id).FirstOrDefault();
            _context.Comments.Remove(delete);
            _context.SaveChanges();
            return RedirectToAction("Comment");
        }
    }
}
