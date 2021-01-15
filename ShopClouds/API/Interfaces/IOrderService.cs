using ShoppingCart.Models;
using System.Threading.Tasks;

namespace ShoppingCart.API.Interfaces
{
    public interface IOrderService
    {
        Task<string> SubmitOrder(Order order);
    }
}
