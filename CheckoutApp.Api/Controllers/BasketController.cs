using CheckoutApp.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutApp.Domain.Interfaces;
using CheckoutApp.Domain.Models;

namespace CheckoutApp.Api.Controllers
{
    [ApiController]
    [Route("controller")]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("/baskets")]
        public async Task<IActionResult> GetAll()
        {
            var baskets = await _basketRepository.GetAll();
            return Ok(baskets);
        }

        [HttpPost("/baskets")]
        public async Task<IActionResult> CreateBasket([FromBody] BasketDto basket)
        {
            if (basket == null)
                return BadRequest();

            await _basketRepository.CreateBasket(basket);
            return Ok();
        }

        [HttpPut("/baskets/{id}/article-line")]
        public async Task<IActionResult> AddArticleToBasket([FromBody] ArticleDto article,string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            if (!int.TryParse(id, out var basketId))
                return BadRequest();

            await _basketRepository.AddItem(basketId, article);
            return Ok();
        }

        [HttpGet("/baskets/{id}")]
        public async Task<IActionResult> GetBasketById(string id)
        {
            if (!int.TryParse(id, out var basketId))
                return BadRequest();

            var basket = await _basketRepository.GetBasketById(basketId);
            return Ok(basket);
        }

        [HttpPatch("/baskets/{id}")]
        public async Task<IActionResult> UpdatePaymentDetails(string id,[FromBody] PaymentDetails payment)
        {
            if (!int.TryParse(id, out var basketId))
                return BadRequest();

            await _basketRepository.UpdatePayment(basketId, payment.Close,payment.Payed);
            return Ok();
        }
    }
}
