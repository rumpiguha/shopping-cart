using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopClouds;
using ShopClouds.API.Services;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShoppingCart.API.Services
{
    public class CurrencyService : BaseService, ICurrencyService
    {
        private readonly ILogger<CurrencyService> _logger;

        public CurrencyService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<CurrencyService> logger) :
             base(httpClient, settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<ExchangeRate>> GetFxRatesAsync()
        {
            var response = await Client.GetAsync("fx-rates");
            _logger.LogInformation($"Service: {nameof(CurrencyService)} Method: {nameof(GetFxRatesAsync)}, Message: API Response Status Code: {response.StatusCode}");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Service: {nameof(CurrencyService)} Method: {nameof(GetFxRatesAsync)}, Message: Error while fetching ExchangeRates.");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Error while fetching ExchangeRates."),
                    ReasonPhrase = "Critical Exception"
                });
            }
            return await response.Content.ReadAsAsync<List<ExchangeRate>>();
        }
    }
}
