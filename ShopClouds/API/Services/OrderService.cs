using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShopClouds;
using ShopClouds.API.Services;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.API.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(HttpClient httpClient, IOptions<ApiSettings> settings, ILogger<OrderService> logger) :
            base(httpClient, settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> SubmitOrder(Order order)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Orders")
            {
                Content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json-patch+json")
            };

            var response = await Client.SendAsync(request);
            _logger.LogInformation($"Service: {nameof(OrderService)} Method: {nameof(SubmitOrder)}, Message: API Response Status Code: {response.StatusCode}");
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : "Order not submitted";
        }
    }
}
