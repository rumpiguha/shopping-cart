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
        private readonly Mock<IProductService> productServiceMock;
        public ProductControllerTest()
        {
            productServiceMock = new Mock<IProductService>();
        }

        [TestMethod]
        public async Task GetProductList_ReturnProducts()
        {
            //Arrange
            productServiceMock.Setup(p => p.GetProductListAsync()).Returns(Task.FromResult(GetMockProducts()));
            var controller = new ProductController(productServiceMock.Object);

            //Act
            var result = await controller.GetProductList() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var products = result.Value as List<Product>;
            Assert.AreEqual(1, products.Count);
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
