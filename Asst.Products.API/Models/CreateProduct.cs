using System.ComponentModel.DataAnnotations;

namespace Asst.Products.API.Models
{
    public class CreateProduct
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int StockAvailable { get; set; }
        [Required]
        public int SellerId { get; set; } 

    }
}
