using System.Threading.Tasks;
using CheckoutApp.Api.Controllers;
using CheckoutApp.Domain.Interfaces;
using CheckoutApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CheckoutApp.UnitTest
{
    [TestClass]
    public class BasketControllerShould
    {
        private Mock<IBasketRepository> _basketRepoMock;
        private BasketController _basketController;

        [TestInitialize]
        public void Setup()
        {
            _basketRepoMock = new Mock<IBasketRepository>();
            _basketController = new BasketController(_basketRepoMock.Object);
        }

        [TestMethod]
        public async Task CreateBasketShould()
        {
            //Arrange 
            var basketDto = new BasketDto()
            {
                Customer = "Customer1",
                PaysVAT = true
            };
            _basketRepoMock.Setup(x=> x.CreateBasket(It.IsAny<BasketDto>())).Verifiable();

            //Act
            var result = await _basketController.CreateBasket(basketDto);

            //Assert
            var response = result as OkResult;
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateNullBasketShould()
        {
            //Act
            var result = await _basketController.CreateBasket(null);

            //Assert
            var response = result as BadRequestResult;
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public async Task GetBasketByIdShould()
        {
            //Arrange 
            var basketId = "1";
            var basketDto = new BasketDto()
            {
                Customer = "Customer1",
                PaysVAT = true
            };
            var basket = new Basket(basketDto);

            _basketRepoMock.Setup(x => x.GetBasketById(It.IsAny<int>())).ReturnsAsync(basket);

            //Act
            var result = await _basketController.GetBasketById(basketId);

            //Assert
            var objectResult = result as OkObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
            var basketResult = objectResult.Value as Basket;
            Assert.IsNotNull(basketResult);
        }

        [TestMethod]
        public async Task GetBasketByWrongFormatIdShould()
        {
            //Arrange 
            var basketId = "s";

            //Act
            var result = await _basketController.GetBasketById(basketId);

            //Assert
            var response = result as BadRequestResult;
            Assert.AreEqual(400, response.StatusCode);
        }
    }
}
