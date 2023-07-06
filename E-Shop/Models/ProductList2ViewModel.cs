using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace E_Shop.Models
{
    public class ProductList2ViewModel
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public decimal Price { get; internal set; }
        public int Stock { get; internal set; }
        public bool Popular { get; internal set; }
        public string CategoryName { get; internal set; }
        public string Description { get; internal set; }
        public bool IsApproved { get; internal set; }
        public string Image { get; internal set; }
        [ValidateNever]//validate dışında tut.
        public IFormFile? ProductImage { get; set; }
        public int CategoryId { get; internal set; }
        public int Quantity { get; set; }
        public int PageCount { get; internal set; }
        public int PageSize { get; internal set; }
        public int CurrentCategory { get; internal set; }
        public int CurrentPage { get; internal set; }
    }
}
