namespace EcommerceWeb.Api.Models.DTO
{
    public class ProductSizeDTO
    {

        public string Size { get; set; }
        public int Quantity { get; set; }
        public List<ProductColorVariantDTO> ProductColorVariant { get; set; }
    }
}
