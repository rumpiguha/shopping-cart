using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class OrderItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
