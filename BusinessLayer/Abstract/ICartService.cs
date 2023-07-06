using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICartService
    {
        //sepet nesnesinin ne olduğunu söyle ve ne eklemek istiyorsun
        void AddToCart(Cart cart, Product product);
        void RemoveFromCart(Cart cart, int productId);
        void RemoveAllFromCart(Cart cart);
        List<CartLine> List(Cart cart);//listeleme için
    }
}
