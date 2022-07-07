using CheckoutApp.Domain.Models;

namespace CheckoutApp.Domain.Extensions
{
    public static class BasketExtension
    {
        public static void CalculateTotalNet(this BasketDto basket, double price)
        {
            basket.TotalNet += price;
        }

        public static void CalculateTotalGross(this BasketDto basket)
        {
            basket.TotalGross = basket.PaysVAT ? basket.TotalNet + basket.TotalNet * 0.1 : basket.TotalNet;
        }
    }
}