using EntityLayer.Entities;

namespace E_Shop.Services
{
    public interface ICartSessionService
    {
        Cart GetCart();//session ı okuyacak operasyon
        void SetCart(Cart cart);//yazma operasyonu
    }
}
