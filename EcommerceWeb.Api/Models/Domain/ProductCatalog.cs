using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ProductCatalog
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public string  Description { get; set; }=string.Empty;

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Stock { get; set; } = 1;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;

        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }

       

       
        public Category Category { get; set; }

    


        public ICollection<ProductImages> ProductImages { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();


    }
}
