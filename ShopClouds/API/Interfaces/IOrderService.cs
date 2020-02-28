using Microsoft.AspNetCore.Http;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.API.Interfaces
{
    public interface IOrderService
    {
        Task<string> SubmitOrder(Order order);
    }
}
