using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ShippingInfo
    {
        [Key]
        public int ShippingID { get; set; }

      

        public string WilayaTo { get; set; }

        public  int ShipingStatus { get; set; }

        public decimal HomeDeliveryPrice { get; set; }

        public decimal OfficeDeliveryPrice { get; set; }


    }
}
