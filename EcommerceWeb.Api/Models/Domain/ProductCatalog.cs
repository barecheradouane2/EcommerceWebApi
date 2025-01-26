using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ProductCatalog
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public string  Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Stock { get; set; }

        public DateTime CreatedAt {  get; set; }

        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }

        // Navigation property

        public Category Category { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();


        public ICollection<ProductImages> ProductImages { get; set; }



    }
}
