using System.Text.Json.Serialization;

namespace EcommerceWeb.Api.Models.DTO
{
    public class AddOrderItemsDTO
    {

     //   public int OrderID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;

        [JsonIgnore]
        public decimal Price { get; set; }

        public decimal TotalItemsPrice => Quantity * Price;
    }
}

