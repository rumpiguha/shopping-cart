using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class OrderItem
    {
        [Required(ErrorMessage = "Product Id is required.")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "Product Quantity is required.")]
        public int Quantity { get; set; }
    }
}
