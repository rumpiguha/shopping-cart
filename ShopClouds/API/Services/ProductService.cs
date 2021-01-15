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
    public class ProductService : BaseService, IProductService
    {
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<ProductService> logger) :
            base(httpClient, settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            var response = await Client.GetAsync("Products");
            _logger.LogInformation($"Service: {nameof(ProductService)} Method: {nameof(GetProductListAsync)}, Message: API Response Status Code: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Service: {nameof(ProductService)} Method: {nameof(GetProductListAsync)}, Message: Error while fetching Clouds.");
                return null;
            }

            return await response.Content.ReadAsAsync<List<Product>>();
        }
    }
}
