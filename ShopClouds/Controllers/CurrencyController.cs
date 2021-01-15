using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    [Route("api/rate")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
        }

        [HttpGet, Route("getRates")]
        public async Task<ActionResult<List<ExchangeRate>>> GetFxRateList()
        {
            return await _currencyService.GetFxRatesAsync();
        }
    }
}