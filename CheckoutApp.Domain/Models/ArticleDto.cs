using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckoutApp.Domain.Models
{
    public class ArticleDto
    {
        public int Id { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        public  double Price { get; set; }
    }
}