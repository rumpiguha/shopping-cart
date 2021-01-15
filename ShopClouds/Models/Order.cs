using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Order
    {
        [Required(ErrorMessage = "Line Items are required.")]
        public List<OrderItem> LineItems { get; set; }
        [Required(ErrorMessage = "Customer Email Address is required.")]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
    }
}
