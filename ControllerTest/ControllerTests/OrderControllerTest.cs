using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Controllers;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestClass]
    public class OrderControllerTest
    {
        private readonly Mock<ILogger<OrderController>> logger;
        private readonly Mock<IOrderService> orderServiceMock;

        public OrderControllerTest()
        {
            logger = new Mock<ILogger<OrderController>>();
            orderServiceMock = new Mock<IOrderService>();
        }

        [TestMethod]
        public async Task Post_SubmitOrder_ReturnSuccess()
        {
            //Arrange
            orderServiceMock.Setup(p => p.SubmitOrder(It.IsAny<Order>())).Returns(Task.FromResult("Order Submitted"));
            var controller = new OrderController(orderServiceMock.Object, logger.Object);

            //Act
            var result = await controller.SubmitOrder(new Order {
                LineItems = new List<OrderItem> {
                    new OrderItem{
                        ProductId= "100AC-001",
                        Quantity = 1
                    }
                },
                CustomerEmail = "test@email.com",
                CustomerName = "test name"
            }) as ObjectResult;

           // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Order Submitted", result.Value);
        }

        [TestMethod]
        public async Task Post_SubmitOrder_Returns_InvalidProduct()
        {
            //Arrange
            orderServiceMock.Setup(p => p.SubmitOrder(It.IsAny<Order>())).Returns(Task.FromResult("Order not submitted"));
            var controller = new OrderController(orderServiceMock.Object, logger.Object);

            //Act
            var result = await controller.SubmitOrder(new Order
            {
                LineItems = new List<OrderItem> {
                    new OrderItem{
                        ProductId= "Invalid Product",
                        Quantity = 0
                    }
                },
                CustomerEmail = "test@email.com",
                CustomerName = "test name"
            }) as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Order not submitted", result.Value);
        }

        [TestMethod]
        public async Task Post_SubmitOrder_Returns_BadRequest_InvalidOrder()
        {
            //Arrange
            orderServiceMock.Setup(p => p.SubmitOrder(It.IsAny<Order>())).Returns(Task.FromResult("Order not submitted"));
            var controller = new OrderController(orderServiceMock.Object, logger.Object);
            var order = new Order();
            //Act
            var validationContext = new ValidationContext(order, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(order, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
            var result = await controller.SubmitOrder(order) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
