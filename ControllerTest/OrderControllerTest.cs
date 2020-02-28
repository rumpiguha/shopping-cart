using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingCart.API.Interfaces;
using ShoppingCart.Controllers;
using ShoppingCart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public async Task PostSubmitOrderSuccess()
        {
            //Arrange
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(p => p.SubmitOrder(It.IsAny<Order>())).Returns(Task.FromResult("Order Submitted"));
            var controller = new OrderController(orderServiceMock.Object);

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
            });

           // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual("Order Submitted", result);
        }

        [TestMethod]
        public async Task PostSubmitOrderFail()
        {
            //Arrange
            var orderServiceMock = new Mock<IOrderService>();
            orderServiceMock.Setup(p => p.SubmitOrder(It.IsAny<Order>())).Returns(Task.FromResult("Order not submitted"));
            var controller = new OrderController(orderServiceMock.Object);

            //Act
            var result = await controller.SubmitOrder(new Order
            {
                LineItems = new List<OrderItem> {
                    new OrderItem{
                        ProductId= "Invalid Prd",
                        Quantity = 0
                    }
                },
                CustomerEmail = "test@email.com",
                CustomerName = "test name"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual("Order not submitted", result);
        }
    }
}
