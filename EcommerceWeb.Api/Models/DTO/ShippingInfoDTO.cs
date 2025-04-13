using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceWeb.Api.Models.DTO
{
    public class ShippingInfoDTO
    {

        [Key]
        public int ShippingID { get; set; }

       

        public string WilayaTo { get; set; }

        [JsonIgnore]

        public int ShipingStatus { get; set; }


        public string ShipingStatusDesc
        {
            get
            {
                return ShipingStatus switch
                {
                    0 => "Not Available",
                    1 => "Available",
                  
                };
            }
        }

        public decimal HomeDeliveryPrice { get; set; }

        public decimal OfficeDeliveryPrice { get; set; }

    }
}
