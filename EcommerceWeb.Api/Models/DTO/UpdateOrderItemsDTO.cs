namespace EcommerceWeb.Api.Models.DTO
{
    public class UpdateOrderItemsDTO
    {

        public int OrderItemsID { get; set; }   
        public int ProductID { get; set; }

        public int Quantity { get; set; }
    }
}
