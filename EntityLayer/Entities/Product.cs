using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Product:IEntity
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

        public virtual List<Cart> Cart { get; set; }
        public virtual List<Sales> Sales { get; set; }

    }
}
