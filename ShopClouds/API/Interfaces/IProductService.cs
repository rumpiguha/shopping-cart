using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart.API.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
    }
}
