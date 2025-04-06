namespace EcommerceWeb.Api.Models.DTO
{
    public class CategoryDTO
    {


        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; }

        
        public List<ProductDTO> ProductCatalog { get; set; } = new List<ProductDTO>();



     
    }
}
