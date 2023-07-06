using EntityLayer.Entities;

namespace E_Shop.Models
{
    public class Product_Category_Model
    {
        public List<Product> Products { get; internal set; }
        public List<Category> Categories { get; internal set; }
        
    }
}
