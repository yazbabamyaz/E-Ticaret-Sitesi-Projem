using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Cart
    {
        

        //ürün yokken null hatası almamak için cartlines ı başlat
        public int Id { get; set; }
        public Cart()
        {
            CartLines = new List<CartLine>();
        }
        //SEPET ELEMANLARI
        public List<CartLine> CartLines { get; set; }
        public decimal Total
        {
            get { return CartLines.Sum(c => c.Product.Price * c.Quantity); }
            //get { return CartLines.Sum(c => 10 * c.Quantity); }
        }
        public decimal Sayi
        {
            get { return CartLines.Sum(c =>c.Quantity); }
            //get { return CartLines.Sum(c => 10 * c.Quantity); }
        }

    }
}
