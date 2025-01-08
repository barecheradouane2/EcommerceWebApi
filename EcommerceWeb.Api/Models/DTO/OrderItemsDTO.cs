using EcommerceWeb.Api.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceWeb.Api.Models.DTO
{
    public class OrderItemsDTO
    {
        [Key]
        public int OrderItemsID { get; set; }

        [JsonIgnore]
        public int OrderID { get; set; }

        public int ProductID { get; set; }

        public string ProductName { get; set; }

        //  public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalItemsPrice { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductID")]
        public ProductCatalog ProductCatalog { get; set; }




    }
}
