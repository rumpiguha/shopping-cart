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

namespace ShopCloudsTest.ServiceTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private OrderService orderService;
        private readonly Mock<ILogger<OrderService>> logger;
        private readonly Mock<HttpMessageHandler> httpMessageHandlerMock;
        private readonly Mock<IOptions<ApiSettings>> options;
        private readonly ApiSettings apiSettings;
        public OrderServiceTests()
        {
            logger = new Mock<ILogger<OrderService>>();
            httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            options = new Mock<IOptions<ApiSettings>>();
            apiSettings = new ApiSettings
            {
                ApiKey = "API-TEST",
                BaseUrl = "http://test.com.au/api/"
            };
        }

        [TestMethod]
        public async Task SubmitOrder_Should_ReturnSuccess_WithValidOrder()
        {
            //Arrange
            options.Setup(x => x.Value).Returns(apiSettings);
            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            orderService = new OrderService(httpClient, options.Object, logger.Object);
            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject("Order Submitted"), Encoding.UTF8, "application/json")
            })
            .Verifiable();

            var order = new Order
            {
                LineItems = new List<OrderItem> { new OrderItem { ProductId = "123", Quantity = 1 } },
                CustomerEmail = "test@email.com",
                CustomerName = "testName"
            };

            //Act
            var result = await orderService.SubmitOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("\"Order Submitted\"", result);
            httpMessageHandlerMock.Protected().Verify(
               "SendAsync",
               Times.AtMostOnce(), // verify number of times SendAsync is called
               ItExpr.Is<HttpRequestMessage>(req =>
               req.Method == HttpMethod.Post  // verify the HttpMethod for request is GET
               && req.RequestUri.ToString() == apiSettings.BaseUrl // verify the RequestUri is as expected
               && req.Headers.GetValues("Accept").FirstOrDefault() == "application/json" //Verify Accept header
               && req.Headers.GetValues("tracking-id").FirstOrDefault() != null //Verify tracking-id header is added
               ),
               ItExpr.IsAny<CancellationToken>()
               );
        }

        [TestMethod]
        public async Task SubmitOrder_Should_ReturnError_WithFailure()
        {
            //Arrange
            options.Setup(x => x.Value).Returns(apiSettings);
            HttpClient httpClient = new HttpClient(httpMessageHandlerMock.Object);
            orderService = new OrderService(httpClient, options.Object, logger.Object);
            httpMessageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync",
            ItExpr.IsAny<HttpRequestMessage>(),
            ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(JsonConvert.SerializeObject("Order not submitted"), Encoding.UTF8, "application/json")
            })
            .Verifiable();

            var order = new Order
            {
                LineItems = new List<OrderItem> { new OrderItem { ProductId = "123", Quantity = 1 } }
            };

            //Act
            var result = await orderService.SubmitOrder(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Order not submitted", result);
            httpMessageHandlerMock.Protected().Verify(
               "SendAsync",
               Times.AtMostOnce(), // verify number of times SendAsync is called
               ItExpr.Is<HttpRequestMessage>(req =>
               req.Method == HttpMethod.Post  // verify the HttpMethod for request is GET
               && req.RequestUri.ToString() == apiSettings.BaseUrl // verify the RequestUri is as expected
               && req.Headers.GetValues("Accept").FirstOrDefault() == "application/json" //Verify Accept header
               && req.Headers.GetValues("tracking-id").FirstOrDefault() != null //Verify tracking-id header is added
               ),
               ItExpr.IsAny<CancellationToken>()
               );
        }
    }
}
