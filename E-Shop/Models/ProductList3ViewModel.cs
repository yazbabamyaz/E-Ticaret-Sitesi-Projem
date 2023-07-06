using EntityLayer.Entities;

namespace E_Shop.Models
{
    public class ProductList3ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Popular { get; set; }
        public bool IsApproved { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public IFormFile? ProductImage { get; set; }
        public string CategoryName { get; internal set; }
    }
}
