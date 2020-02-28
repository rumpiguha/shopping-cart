using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ShopClouds;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        private ApiSettings ApiSettings { get; set; }

        public OrderService(HttpClient httpClient, IOptions<ApiSettings> settings, ILoggerFactory logFactory)
        {
            ApiSettings = settings.Value;
            httpClient.BaseAddress = new Uri(ApiSettings.BaseUrl, UriKind.Absolute);
            _httpClient = httpClient;
            _logger = logFactory.CreateLogger<OrderService>();
        }
        public async Task<string> SubmitOrder(Order order)
        {
            _httpClient.DefaultRequestHeaders.Add("api-key", ApiSettings.ApiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Orders")
            {
                Content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json-patch+json")
            };

           var response =  await _httpClient.SendAsync(request);
            _logger.LogInformation($"Calling API: {_httpClient.BaseAddress}Orders");
            _logger.LogInformation($"API Response Status Code: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsStringAsync().Result;
            return "Order not submitted";

        }



    }
}
