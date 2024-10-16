using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Asst.Products.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public int StockAvailable { get; set; }
        [Required]
        public int SellerId { get; set; }

        [ForeignKey("SellerId")]
        [JsonIgnore]
        public Seller Seller { get; set; }

    }
}
