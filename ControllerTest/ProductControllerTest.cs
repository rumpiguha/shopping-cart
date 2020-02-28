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
    public class ProductControllerTest
    {
        [TestMethod]
        public async Task GetReturnProductAsync()
        {
            //Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(p => p.GetProductListAsync()).Returns(Task.FromResult(GetMockProducts()));
            var controller = new ProductController(productServiceMock.Object);

            //Act
            var result = await controller.GetProductList();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Product>>));
            List<Product> products = result.Value as List<Product>;
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public async Task GetEmptyProductAsync()
        {
            //Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(p => p.GetProductListAsync()).Returns(Task.FromResult(new List<Product>()));
            var controller = new ProductController(productServiceMock.Object);

            //Act
            var result = await controller.GetProductList();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<Product>>));
            List<Product> products = result.Value as List<Product>;
            Assert.AreEqual(0, products.Count);
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
