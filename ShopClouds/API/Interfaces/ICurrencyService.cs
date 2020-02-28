using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.API.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<ExchangeRate>> GetFxRatesAsync();
    }
}
