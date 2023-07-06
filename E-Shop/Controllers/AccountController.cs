using DataAccessLayer.Context;
using E_Shop.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security;
using System.Net.Mail;
using System.Net;

namespace E_Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context2;
        private string code = null;

        public AccountController(AppDbContext context)
        {
            _context2 = context;
        }

        //public object FormsAuthentication { get; set; }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(User data)
        {
            var user = _context2.Users.FirstOrDefault(x => x.UserName == data.UserName && x.Password == data.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("userid",user.Id.ToString());
                HttpContext.Session.SetString("kullanici",user.UserName.ToString());

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,data.UserName),
                    //new Claim(ClaimTypes.Role,"Admin")
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.Email,user.Email)                    
				};
                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                
                await HttpContext.SignInAsync(principal);               
               
                return RedirectToAction("index", "Home2");

            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Register(User data)
        {
            //if (ModelState.IsValid)
            //{
				_context2.Users.Add(data);
				data.Role = "User";
				_context2.SaveChanges();
				return RedirectToAction("Login");
			//}
   //         else
   //         {
   //             ModelState.AddModelError("", "Hatalı");
   //             return View("Login",data);
   //         }
            
			
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }
        public IActionResult ForgotPassword()
        {

            return View();
        }
		public IActionResult ResetPassword()
		{

			return View();
		}
		public IActionResult SendCode( string Email)
		{
            var user = _context2.Users.FirstOrDefault(x => x.Email.Equals(Email));
            if (user != null)
            {
                var sifre= getCode();
                user.Password = sifre;
                user.RePassword = sifre;
				_context2.SaveChanges();
				TempData.Add("message", string.Format("Şifre mail adresine gönderildi."));
                //mail gönderme
                string text = "<h1>Sıfırlama kodunuz:</h1>" + sifre;
                string subject = "Şifre Sıfırlama";
                MailMessage msg = new MailMessage("testprojem1@gmail.com",Email,subject,text);
                msg.IsBodyHtml = true;//html kodlarını kullanabilmek için.
                SmtpClient sc = new SmtpClient("smtp.gmail.com",587);//gmail için geçerli olan ayarlar.host ve port
                sc.UseDefaultCredentials = false;
                NetworkCredential cre = new NetworkCredential("testprojem1@gmail.com", "glojiemjcjunnhtw");// -- glojiemjcjunnhtw
				sc.Credentials = cre;
                sc.EnableSsl = true;
                sc.Send(msg);
				//hata verir sebebi:Google hesabınızın dışarıdan harici bir uygulama tarafından kullanılmasına izin verilmiyor olmasıdır.
				;
			}
            else
            {
				TempData.Add("message", string.Format("Sistemde yazdığınız  mail adresi bulunamadı."));
				
			}
            return RedirectToAction("Login","Account");
			
		}
		public string getCode()
        {
            //6 haneli random sayı
            if (code==null)
            {
                Random rnd = new Random();
                code = "";
                for (int i = 0; i < 6; i++)
                {
                    char tmp =Convert.ToChar(rnd.Next(48, 58));//Ascı code elde etcez. 1-9
                    code += tmp;
                }

            }
            return code;

        }
       
        public IActionResult ListUser()
        {
            var user=_context2.Users.Where(x => x.Role == "User").ToList();
            var model = new UserViewModel
            {
                ListUser = user
            };
            return View(model);
        }
        public IActionResult UserDelete(int id)
        {
            var userid = _context2.Users.Where(x=>x.Id==id).FirstOrDefault();
            _context2.Users.Remove(userid);
            _context2.SaveChanges();
            return RedirectToAction("ListUser");
            
        }


    }
}
