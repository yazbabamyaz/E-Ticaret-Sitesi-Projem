using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
       
        List<Product> GetCategory(int categoryId);//categoriye göre ürünleri getir.
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        Product ProductGetById(int id);
        List<Product> GetPopular();
       

    }
}
