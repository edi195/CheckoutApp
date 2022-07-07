namespace CheckoutApp.Domain.Models
{
    public class Article
    {
        public string Item { get; set; }
        public double Price { get; set; }

        public Article(ArticleDto article)
        {
            Item = article.Item;
            Price = article.Price;
        }
    }
}