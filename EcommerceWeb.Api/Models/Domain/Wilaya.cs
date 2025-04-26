using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Wilaya
    {
        [Key]
        public int WilayaID { get; set; }

        public string WilayaName { get; set; }

        [ForeignKey("ShippingID")]

        public int ShippingID { get; set; }


        public ShippingInfo ShippingInfo { get; set; }



    
    }
}
