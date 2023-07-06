using EntityLayer.Entities;

namespace E_Shop.Models
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; internal set; }
        public List<Product> products { get; internal set; }
    }
}
