using EntityLayer.Entities;

namespace E_Shop
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; internal set; }
        public List<Product> products { get; internal set; }//sonradan ekledim
        public List<Category>Categories { get; internal set; }
        //public Category CategoryName { get; set; }
       
    }
}