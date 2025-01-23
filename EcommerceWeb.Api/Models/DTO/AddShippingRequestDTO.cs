namespace EcommerceWeb.Api.Models.DTO
{
    public class AddShippingRequestDTO
    {

        public string WilayaFrom { get; set; }

        public string WilayaTo { get; set; }

        public int ShipingStatus { get; set; } = 0;


        public decimal HomeDeliveryPrice { get; set; }

        public decimal OfficeDeliveryPrice { get; set; }


    }
}
