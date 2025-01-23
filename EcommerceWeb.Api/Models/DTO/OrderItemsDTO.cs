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

        public int Quantity { get; set; }

        // Navigation property to ProductCatalog to access the price
        [JsonIgnore]
        public ProductCatalog ProductCatalog { get; set; }

        // Calculated property for TotalItemsPrice based on the Price from ProductCatalog
        public decimal TotalItemsPrice
        {
            get;set;
        }

        // This would now return the price of the product from ProductCatalog
        public decimal Price
        {
            get;set;
        }
    }
}
