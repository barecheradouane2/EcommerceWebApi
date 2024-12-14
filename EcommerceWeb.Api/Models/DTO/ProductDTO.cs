using EcommerceWeb.Api.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.DTO
{
    public class ProductDTO
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public DateTime CreatedDate { get; set; }

       

        public CategoryDTO Category { get; set; }









    }
}
