using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Order
    {
        public List<OrderItem> LineItems { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerName { get; set; }
    }
}
