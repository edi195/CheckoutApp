using System.Collections.Generic;

namespace CheckoutApp.Domain.Models
{
    public class Basket
    {
        public string Customer { get; set; }
        public bool PaysVAT { get; set; }
        public double TotalNet { get; set; }
        public List<Article> Items { get; set; }
        public double TotalGross { get; set; }
        public bool Close { get; set; }
        public bool Payed { get; set; }

        public Basket(BasketDto basket)
        {
            Customer = basket.Customer;
            PaysVAT = basket.PaysVAT;
            TotalNet = basket.TotalNet;
            TotalGross = basket.TotalGross;
            Close = basket.Close;
            Payed = basket.Payed;
            Items = new List<Article>();
        }
    }
}