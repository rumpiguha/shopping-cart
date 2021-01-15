using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace ShopClouds.API.Services
{
    public class BaseService
    {
        protected HttpClient Client { get; }
        protected ApiSettings ApiSettings { get; set; }

        protected BaseService(HttpClient client, IOptions<ApiSettings> settings)
        {
            ApiSettings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Client.BaseAddress = new Uri(ApiSettings.BaseUrl, UriKind.Absolute);
            Client.DefaultRequestHeaders.Add("api-key", ApiSettings.ApiKey);
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
