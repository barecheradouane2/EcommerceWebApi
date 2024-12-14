using EcommerceWeb.Api.Models.Domain;

namespace EcommerceWeb.Api.Models.DTO
{
    public class UpdateProductRequestDTO
    {
        public string ProductName { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Stock { get; set; }

       public DateTime CreatedAt { get; set; }

        public int CategoryID { get; set; }

      
    }
}
