namespace EcommerceWeb.Api.Models.DTO
{
    public class CategoryDTO
    {


        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;



        public string ImagePath { get; set; } = string.Empty;


        public List<ProductDTO> ProductCatalog { get; set; } = new List<ProductDTO>();



     
    }
}
