using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CheckoutApp.Domain.Models
{
    public class BasketDto
    {
        public int Id { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        public bool PaysVAT { get; set; }
        public double TotalNet { get; set; }
        public double TotalGross { get; set; }
        public bool Close { get; set; }
        public bool Payed { get; set; }
    }
}