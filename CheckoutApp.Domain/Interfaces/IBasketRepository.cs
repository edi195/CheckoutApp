using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CheckoutApp.Domain.Models;

namespace CheckoutApp.Domain.Interfaces
{
    public interface IBasketRepository: IDisposable
    {
        Task<IEnumerable<Basket>> GetAll();
        Task<Basket> GetBasketById(int basketId);
        Task CreateBasket(BasketDto basket);
        Task AddItem(int basketId, ArticleDto article);
        Task UpdatePayment(int basketId, bool close, bool payed);
    }
}