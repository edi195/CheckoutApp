namespace CheckoutApp.Domain.Models
{
    public class BasketArticleDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BasketId { get; set; }
    }
}