using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using E_Shop.Models;
using E_Shop.Services;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace E_Shop.Controllers
{
    public class CartController : Controller
    {
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        private IProductService _productService;       
        private readonly AppDbContext _context;

        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService, AppDbContext context)
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
            _productService = productService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCart(int productId)
        {

            if (User.Identity.IsAuthenticated)
            {
                //gelen ıd ye sahip ürünü db den alalım.Sesion a eklemek için
                var productToAdded = _productService.ProductGetById(productId);
                //şimdi sepete ulaşcaz. httpcontext session yaparız ama yapmicaz biz zaten onun için cartsession service yaptık 
                var cart = _cartSessionService.GetCart();//sepet sessionını verecek
                                                         //bu cart nesnesine bir tane ürün ekleyelim.
                _cartService.AddToCart(cart, productToAdded);//sepete şu ürünü ekle dedik
                _cartSessionService.SetCart(cart);

                TempData.Add("message", string.Format("{0} ürünü sepete eklendi.", productToAdded.Name));
                return RedirectToAction("index", "Home2");
            }
            return RedirectToAction("Login", "Account");
            
           
            
            
        }
        public ActionResult List()
        {
            //TempData["kullanici"]= HttpContext.Session.GetString("kullanici");
           
            var cart = _cartSessionService.GetCart();
            CartListViewModel cartListViewModel = new CartListViewModel
            {
                Cart = cart

            };
            return View(cartListViewModel);
        }

        public IActionResult Remove(int productId)
        {
            var cart = _cartSessionService.GetCart();//session nesnesine ulaşmak lazım.elimizde bir nesne mevcut.
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);
            //session çektik sonra silme işlemi ve sonra tekrardan cart ı session a yazdık
            TempData.Add("message2", string.Format("Ürün  sepetten silindi."));
            
            
            
            return RedirectToAction("List");
        }
        public IActionResult Complete()
        {
             
            if (User.Identity.IsAuthenticated)
            {
                var kullanici = User.Identity.Name.ToString();
                var kid= HttpContext.Session.GetString("userid");
                var cart = _cartSessionService.GetCart();                
                
                CartListViewModel cartListViewModel = new CartListViewModel
                {
                    Cart = cart

                };
                
                //Sepetimdeki ürünleri satış tablosuna ekledim.Ve stok güncellemesi yaptım.
                foreach (var item in cartListViewModel.Cart.CartLines)
                {
                    var satis = new Sales
                    {
                        UserId = Convert.ToInt32(kid),
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = item.Product.Price,
                        Image = item.Product.Image,
                        Date = DateTime.Now
                    };
                    _context.Sales.Add(satis);
                    _context.SaveChanges();
                    //var updatedValue=_context.Products.Where(x=>x.Id==item.Product.Id).ToList();
                    var updatedValue = _context.Products.Find(item.Product.Id);
                    updatedValue.Stock -=item.Quantity;
                    _context.Products.Update(updatedValue);
                    _context.SaveChanges();

                }

                
                //Satış olduktan sonra sepeti temizlemek için:
                cart = _cartSessionService.GetCart();
                _cartService.RemoveAllFromCart(cart);//sepeti boşalttım.
                _cartSessionService.SetCart(cart);
                               
                TempData.Add("message", string.Format("Siparişiniz en kısa sürede kargoya verilecektir."));
                
                return RedirectToAction("index","Home2");
                
                



            }
            
            return View();
        }
    }
}
