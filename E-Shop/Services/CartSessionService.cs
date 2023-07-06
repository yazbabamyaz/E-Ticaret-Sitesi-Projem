using E_Shop.ExtensionMethods;
using EntityLayer.Entities;

namespace E_Shop.Services
{
    public class CartSessionService : ICartSessionService
    {

        private IHttpContextAccessor _httpContextAccessor;

        public CartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCart()
        {
            //sepetteki cart nesnesini getir carta deserialize et.
            //yoksa ifin içinde oluştur öyle getir.
            Cart cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");

            if (cartToCheck == null)
            {
                //ilk ekleme-hiç ürün eklenmemiş-
                _httpContextAccessor.HttpContext.Session.SetObject("cart", new Cart());
                cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            }
            return cartToCheck;
        }

        public void SetCart(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObject("cart", cart);
        }
    }
}
