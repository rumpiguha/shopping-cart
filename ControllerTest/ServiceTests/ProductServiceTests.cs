using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ShopClouds;
using ShoppingCart.API.Services;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopCloudsTest.ServiceTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private ProductService productService;
        private readonly Mock<ILogger<ProductService>> logger;
        private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
        private readonly Mock<IOptions<ApiSettings>> options;
        private readonly ApiSettings apiSettings;
        public ProductServiceTests()
        {
            logger = new Mock<ILogger<ProductService>>();
            httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            options = new Mock<IOptions<ApiSettings>>();
            apiSettings = new ApiSettings
            {
                ApiKey = "API-TEST",
                BaseUrl = "http://test.com.au/api/"
            };
        }

        [TestMethod]
        public async Task GetProductListAsync_Should_ReturnProductsList()
        {
            //Arrange
            options.Setup(x => x.Value).Returns(apiSettings);
            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            productService = new ProductService(httpClient, options.Object, logger.Object);
            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(GetMockProducts()), Encoding.UTF8, "application/json")
            })
            .Verifiable();

            //Act
            var result = await productService.GetProductListAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            httpMessageHandlerMock.Protected().Verify(
               "SendAsync",
               Times.AtMostOnce(), // verify number of times SendAsync is called
               ItExpr.Is<HttpRequestMessage>(req =>
               req.Method == HttpMethod.Get  // verify the HttpMethod for request is GET
               && req.RequestUri.ToString() == apiSettings.BaseUrl // verify the RequestUri is as expected
               && req.Headers.GetValues("Accept").FirstOrDefault() == "application/json" //Verify Accept header
               && req.Headers.GetValues("tracking-id").FirstOrDefault() != null //Verify tracking-id header is added
               ),
               ItExpr.IsAny<CancellationToken>()
               );
        }

        [TestMethod]
        public async Task GetProductListAsync_Should_Return_Null()
        {
            //Arrange
            options.Setup(x => x.Value).Returns(apiSettings);
            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            productService = new ProductService(httpClient, options.Object, logger.Object);
            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = null
            })
            .Verifiable();

            //Act
            var result = await productService.GetProductListAsync();

            //Assert
            Assert.IsNull(result);
        }

        private List<Product> GetMockProducts()
        {
            return new List<Product> {
                new Product{
                ProductId = "1",
                Name = "Test Product",
                Description = "Test Descp",
                 MaximumQuantity = 1,
                UnitPrice = 10}
            };
        }
    }
}
