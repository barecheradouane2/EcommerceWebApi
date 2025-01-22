using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class OrderItems
    {

        [Key]
        public int OrderItemsID { get; set; }

        [ForeignKey("OrderID")]
        public int OrderID { get; set; }

     

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalItemsPrice { get; set; }

        // Navigation property

        [ForeignKey("ProductID")]

        public int ProductID { get; set; }
        public ProductCatalog ProductCatalog { get; set; }
    }
}
