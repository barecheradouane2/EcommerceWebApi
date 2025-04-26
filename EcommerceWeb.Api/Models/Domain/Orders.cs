using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; } = 0;

        public int OrderStatus { get; set; } = 0;

        public string FullName { get; set; }

        public string TelephoneNumber { get; set; }

        [ForeignKey("WilayaID")]
        public int WilayaID { get; set; }

        public Wilaya Wilaya { get; set; }


        [ForeignKey("CommuneID")]
        public int CommuneID { get; set; }

        public Commune Commune { get; set; }

        public string OrderAddress { get; set; }

        //public int DiscountCodeID { get; set; }



        public int ShippingStatus { get; set; } = 0;

        // Navigation properties
        [ForeignKey("ShippingID")]  // Explicitly link the foreign key

        public int ShippingID { get; set; }
        public ShippingInfo ShippingInfo { get; set; }

        [ForeignKey("DiscountCodeID")]  // Explicitly link the foreign key
        //public DiscountCodes DiscountCodes { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
