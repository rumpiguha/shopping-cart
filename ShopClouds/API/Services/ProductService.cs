using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShopClouds;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingCart.API.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        private ApiSettings ApiSettings { get; set; }

        public ProductService(HttpClient httpClient, IOptions<ApiSettings> settings, ILoggerFactory logFactory)
        {
            ApiSettings = settings.Value;
            httpClient.BaseAddress = new Uri(ApiSettings.BaseUrl, UriKind.Absolute);
            _httpClient = httpClient;
            _logger = logFactory.CreateLogger<ProductService>();
        }

        public async Task<List<Product>> GetProductListAsync()
        {
           
            _httpClient.DefaultRequestHeaders.Add("api-key", ApiSettings.ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync("Products");

            _logger.LogInformation($"Calling API: {_httpClient.BaseAddress}Products");
            _logger.LogInformation($"API Response Status Code: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Product>>();
            }
            return new List<Product>();
        }
    }
}
