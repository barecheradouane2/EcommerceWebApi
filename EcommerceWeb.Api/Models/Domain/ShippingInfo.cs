using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ShippingInfo
    {
        [Key]
        public int ShippingID { get; set; }

        [ForeignKey("WilayaID")]
        public int WilayaID { get; set; }

        public Wilaya Wilaya { get; set; }


        //public string WilayaTo { get; set; }

        public  int ShipingStatus { get; set; }

        public decimal HomeDeliveryPrice { get; set; }

        public decimal OfficeDeliveryPrice { get; set; }


    }
}
