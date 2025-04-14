using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.DTO
{
    public class ProductUplodadImageDTO
    {
        [Required]
        public List<IFormFile> ImageFile { get; set; }
       

        public string ProductName { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Stock { get; set; } 

        //public DateTime CreatedAt { get; set; }

        public int CategoryID { get; set; }

        // You’ll send this as JSON string in the request (e.g., using FormData from frontend)
        public string ProductSizes { get; set; } = "[]";



    }
}
