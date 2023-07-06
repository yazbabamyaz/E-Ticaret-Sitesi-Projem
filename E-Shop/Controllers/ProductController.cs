using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using E_Shop.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace E_Shop.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private AppDbContext _context;


        public ProductController(IProductService productService, IMapper mapper, AppDbContext context, ICategoryService categoryService)
        {
            _productService = productService;

            _mapper = mapper;
            _context = context;
            _categoryService = categoryService;
        }

       

        public IActionResult Index()
        {
            //-best practise - view a direk product gönderme. viewmodel gönder
            
            var products=_productService.GetAll().ToList();
            ProductListViewModel model = new ProductListViewModel
            {
                Products = products 
                //ekstradan başka şeyler de ekleyip gönderebilirim.
            };
            return View(model);
        }

        [Route("Product/Details/{id}/{name}")]
        public IActionResult Details(int id)
        {
            //TempData["kullanici"] = HttpContext.Session.GetString("kullanici");
           
            //oturum açan kullanıcının id'sine göre ürüne yaptığı yorumlar için.
            ViewBag.data = HttpContext.Session.GetString("userid");
            var product = _productService.ProductGetById(id);
            var cat = _categoryService.CategoryGetById(product.CategoryId);
            DetailsViewModel model = new DetailsViewModel
            {
                Products = product,
                Categories = cat
                //ekstradan başka şeyler de ekleyip gönderebilirsin.
            };
            //ürünid ye göre yapılmış yorumları getirelim.

            var commentViewModel = new ProductCommentViewModel()
            {
                Comments = _context.Comments.Where(x => x.ProductId == id).Include(x => x.User).ToList()

            };
            ViewBag.Comment = commentViewModel;

            //BU ŞEKİLDE DE YORUMLARI ÇEKİP GÖNDEREBİLİRDİM.
            //var yorum = _context.Comments.Where(x => x.ProductId == id).ToList();           
            //ViewBag.yorum = yorum;

            return View(model);
        }
        [HttpPost]
        public IActionResult Comment(Comment data)
        {
            if (User.Identity.IsAuthenticated)
            {
                _context.Comments.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Index","Home2");
            }
            return View(); 
        }
    }
}
