using DataAccessLayer.Context;
using E_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Shop.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Update()
        {
            var userName = HttpContext.Session.GetString("kullanici");
            var data = _context.Users.FirstOrDefault(x => x.UserName == userName);
            var model = new UserViewModel
            {
                Name = data.Name,
                Email = data.Email,
                SurName= data.SurName,
                UserName= data.UserName,
                Password= data.Password,
                RePassword= data.RePassword,
            
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(UserViewModel data)
        {

            var userName = HttpContext.Session.GetString("kullanici");

            var updatedValue = _context.Users.Where(x => x.UserName == userName).FirstOrDefault();

            updatedValue.Name = data.Name;
            updatedValue.Email = data.Email;
            updatedValue.SurName = data.SurName;
            updatedValue.Password = data.Password;
            updatedValue.RePassword = data.RePassword;
            updatedValue.UserName = data.UserName;
            updatedValue.Role = "User";
            //_context.Users.Update(updatedValue);
            _context.SaveChanges();
            return RedirectToAction("Index","Home2");
            
        }
    }
}
