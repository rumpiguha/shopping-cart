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
    public class CurrencyControllerTest
    {
        [TestMethod]
        public async Task GetFxList()
        {
            //Arrange
            var currencyServiceMock = new Mock<ICurrencyService>();
            currencyServiceMock.Setup(p => p.GetFxRatesAsync()).Returns(Task.FromResult(GetRateList()));
            var controller = new CurrencyController(currencyServiceMock.Object);

            //Act
            var result = await controller.GetFxRateList();

           // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ActionResult<List<ExchangeRate>>));
            List<ExchangeRate> list = result.Value as List<ExchangeRate>;
            Assert.AreEqual(1, list.Count);
        }

        private List<ExchangeRate> GetRateList()
        {
            return new List<ExchangeRate> {
                new ExchangeRate{
                    SourceCurrency = "srcCurrency",
                    Rate = 0.5,
                    TargetCurrency = "trgtCurrency"
                }
            };
        }
    }
}
