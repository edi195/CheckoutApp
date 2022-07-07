using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckoutApp.Domain.Extensions;
using CheckoutApp.Domain.Interfaces;
using CheckoutApp.Domain.Models;

namespace CheckoutApp.DataAccess
{
    public class BasketRepository: IBasketRepository
    {
        private readonly BasketContext _context;

        public BasketRepository(BasketContext context)
        {
            _context = context;
        }


        public async Task<Basket> GetBasketById(int basketId)
        {
            var basketDto = await _context.Baskets.FirstOrDefaultAsync(b => b.Id == basketId);
            var itemIds = _context.BasketArticles.Where(b => b.BasketId == basketId)
                .Select(b => b.ItemId).ToList();

            return PopulateBasket(basketDto, itemIds);
        }

        public async Task<IEnumerable<Basket>> GetAll()
        {
            var baskets = new List<Basket>();
            var basketIds =  _context.Baskets.Select(b => b.Id).ToList();
            var listOfTasks = new List<Task<Basket>>();
            basketIds.ForEach(async id =>
            {
                listOfTasks.Add(GetBasketById(id));
            });
            baskets.AddRange(await Task.WhenAll<Basket>(listOfTasks));
            return baskets;
        }

        private Basket PopulateBasket(BasketDto basketDto, List<int> itemIds)
        {
            var basket = new Basket(basketDto);
            itemIds.ForEach(async id => 
            {
                var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id);
                basket.Items.Add(new Article(article));
            });

            return basket;
        }

        public async Task CreateBasket(BasketDto basket)
        {
            await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
        }

        public async Task AddItem(int basketId, ArticleDto article)
        {
            var articleId = await AddArticle(article);
            var basket = _context.Baskets.FirstOrDefault(b=>b.Id==basketId);

            if (basket == null)
                return;
            basket.CalculateTotalNet(article.Price);
            basket.CalculateTotalGross();
            await _context.BasketArticles.AddAsync(new BasketArticleDto(){BasketId = basket.Id, ItemId = articleId});
            await _context.SaveChangesAsync();
        }

        private async Task<int> AddArticle(ArticleDto article)
        {
            var id = 0;
            var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Item == article.Item);
            if (entity != null)
                id = entity.Id;
            else
            {
                entity = (await _context.Articles.AddAsync(article)).Entity;
                await _context.SaveChangesAsync();
                id = entity.Id;
            }
            return id;
        }

        public async Task UpdatePayment(int basketId, bool close, bool payed)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(b=> b.Id == basketId);
            basket.Close = close;
            basket.Payed = payed;
            await _context.SaveChangesAsync();
        }



        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}