using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using E_Shop.Models;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Data;
using System.Drawing.Printing;
using System.Linq;

namespace E_Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        //private AppProContext _context;
        private AppDbContext _context2;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;


        public AdminProductController(IProductService productService, ICategoryService categoryService, IFileProvider fileProvider, IMapper mapper, AppDbContext context2)
        {
            _productService = productService;
            _categoryService = categoryService;
            
            _fileProvider = fileProvider;
            _mapper = mapper;
            _context2 = context2;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int page = 1, int category = 0)
        {

           
            var productsById = _productService.GetCategory(category);
            //Kategoriye göre sayfalama
            int pagesize = 5;

            var model = new ProductListViewModel()
            {
                products = _context2.Products.Include(x => x.Category).Skip((page - 1) * pagesize).Take(pagesize).ToList()
        };

           
            ViewBag.PageCount = (int)Math.Ceiling(productsById.Count/(double)pagesize);
            ViewBag.PageSize = pagesize;
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentPage = page;
            return View(model);

        }
        public IActionResult Create() 
        {
            //drop doldurmanın 2 yolu:

            List<SelectListItem> deger1 = (from i in _categoryService.GetAll().ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()

                                           }).ToList();


            //List<SelectListItem> deger1 = (from i in _context2.Categories.ToList()
            //                               select new SelectListItem
            //                               {
            //                                   Text = i.Name,
            //                                   Value = i.Id.ToString()

            //                               }).ToList();
            ViewBag.categorySelect = deger1;//controllerdan view e veri taşıyorum.
                                          //var categories=_context.Categories.ToList();

            //ViewBag.categorySelect = new SelectList(categories, "Id", "Name");
          
           
            return View();
        }

        [HttpPost]
        public  IActionResult Create(ProductList3ViewModel product)
        {
            var products = _mapper.Map<Product>(product);
            if (product.ProductImage != null && product.ProductImage.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");//ana dizin
                var images = root.First(x => x.Name == "images");//wwwroot/images klasörü
                                                                 //guid değer + uzantıyı alıp birleştirdik.
                var randomImageName = Guid.NewGuid() + Path.GetExtension(product.ProductImage.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);//images klasörünün gerçek yolu na fileupload dan gelen dosya ismini ekle

                ////resmi kaydetmek için filestream oluştur.Path e bak yoksa oluştur.
                using var stream = new FileStream(path, FileMode.Create);

                //await product.ProductImage.CopyToAsync(stream);/*asenkron metot kullanımı*/
                product.ProductImage.CopyTo(stream);

                ////şuan resmi kaydettik sıra yolunu db ye kaydetmekte.
                products.Image = randomImageName;

            }
            else
            {
                products.Image = "NoImage.jpg";
            }

            _productService.Add(products);
            //_context2.Products.Add(products);
            //_context2.SaveChanges();

            TempData["status"] = "Ürün Başarıyla Eklendi...";
            return RedirectToAction("Index");


            //Product product1 = new Product()
            //{
            //    Name = product.Name,
            //    Price = product.Price,
            //    Stock = product.Stock,
            //    Popular = product.Popular,

            //};

        }

        public IActionResult Delete(int id)     
        {
            _productService.Delete(id);
            TempData["status_Remove"] = "Ürün Başarıyla Silinmiştir...";
            return RedirectToAction("Index");

            //return View();

        }
        public IActionResult Update(int id) 
        {

            List<SelectListItem> deger1 = (from i in _categoryService.GetAll().ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()

                                           }).ToList();
            ViewBag.categorySelect = deger1;


            var updatedValue = _productService.ProductGetById(id); 
            var viewModel=_mapper.Map<ProductUpdateViewModel>(updatedValue);
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel product)
        {
            
            var updatedValue=_productService.ProductGetById(product.Id);
            if (product.ProductImage != null && product.ProductImage.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");//ana dizin
                var images = root.First(x => x.Name == "images");//wwwroot/images klasörü
                                                                 //guid değer + uzantıyı alıp birleştirdik.
                var randomImageName = Guid.NewGuid() + Path.GetExtension(product.ProductImage.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);//images klasörünün gerçek yolu na fileupload dan gelen dosya ismini ekle

                ////resmi kaydetmek için filestream oluştur.Path e bak yoksa oluştur.
                using var stream = new FileStream(path, FileMode.Create);

                //await product.ProductImage.CopyToAsync(stream);/*asenkron metot kullanımı*/
                product.ProductImage.CopyTo(stream);

                ////şuan resmi kaydettik sıra yolunu db ye kaydetmekte.
                updatedValue.Image = randomImageName;
            }
            else
            {
                updatedValue.Image =product.Image;
            }



            updatedValue.Name= product.Name;
            updatedValue.Description= product.Description;
            updatedValue.IsApproved= product.IsApproved;
            updatedValue.Popular= product.Popular;
            updatedValue.Price= product.Price;
            updatedValue.Stock= product.Stock;
            
            updatedValue.CategoryId= product.CategoryId;

            _productService.Update(updatedValue);
            TempData["status_Remove"] = "Ürün Başarıyla Güncellenmiştir...";
            return RedirectToAction("Index");
            
        }
        public IActionResult CriticalStock()
        {
            var model = new CriticalViewModel
            {
                Critical= _context2.Products.OrderByDescending(x=>x.Stock).Where(x => x.Stock <= 50).ToList()
        };
            
            return View(model); 
        }
        //public PartialViewResult StockCount()
        //{
        //    //if (User.Identity.IsAuthenticated)
        //    //{
        //        var count = _context2.Products.Where(x => x.Stock < 50).Count();
        //    TempData["count"]= count;
        //        var count2=_context2.Products.Where(x=>x.Stock==50).Count();
        //        ViewBag.count2= count2; 
        //    //}
        //    return PartialView();
        //}

    }
}
