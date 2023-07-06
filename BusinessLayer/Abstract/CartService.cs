using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public class CartService:ICartService
    {

        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.Id == product.Id);
            if (cartLine != null)//sepette ürün varsa
            {
                cartLine.Quantity++;
                return;
            }
            cart.CartLines.Add(new CartLine { Product = product, Quantity = 1 });
        }

        public List<CartLine> List(Cart cart)
        {
            return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            
                cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.Id == productId));         
            
        }
        public void RemoveAllFromCart(Cart cart)
        {

            //cart.CartLines.Remove(cart.CartLines.FirstOrDefault());
            //cart.CartLines.RemoveAll();
            cart.CartLines.RemoveRange(0,cart.CartLines.Count());

        }

    }
}
