namespace EcommerceWeb.Api.Models.DTO
{
    public class UpdateOrderItemsDTO
    {

        public int OrderItemsID { get; set; }   
        public int ProductID { get; set; }

        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
