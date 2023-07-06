using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
       
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(int productId)
        {
            //Ya da 
            //var deletedValue =_productDal.Get(p=>p.Id == productId);
            // _productDal.Delete(deletedValue);
            
            //delete metodu benden Product ister bende ise productıd var.
            _productDal.Delete(new Product { Id=productId});
        }

       

        public List<Product> GetAll()
        {
            //bu kodlar çalışır fakat SOLID e ters. üst katman alt katmanı newleyemez.değişim odaklı çalışıyorsan uygun değil 
            //EfProductDal productDal = new EfProductDal();
            //return productDal.GetList();
            return _productDal.GetList();
        }

        public List<Product> GetCategory(int categoryId)
        {
            //linq şartı gönderdim.
            return _productDal.GetList(p=>p.CategoryId== categoryId ||categoryId==0);
        
        }

        

        public List<Product> GetPopular()
        {
           return _productDal.GetList(x=>x.Popular==true).ToList();
           
        }

        public Product ProductGetById(int id)
        {
            return _productDal.Get(p=>p.Id== id);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
