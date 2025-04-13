namespace EcommerceWeb.Api.Models.DTO
{
    public class AddShippingRequestDTO
    {

     

        public string WilayaTo { get; set; }

        public int ShipingStatus { get; set; } = 1;


        public decimal HomeDeliveryPrice { get; set; }

        public decimal OfficeDeliveryPrice { get; set; }


    }
}
