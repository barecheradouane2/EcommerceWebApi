using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public int OrderStatus { get; set; }

        public string FullName { get; set; }

        public string TelephoneNumber { get; set; }

        public string Wilaya { get; set; }

        public string Commune { get; set; }

        public string OrderAddress { get; set; }

        public int DiscountCodeID { get; set; }

        public int ShippingID { get; set; }

        public int ShippingStatus { get; set; }

        // Navigation properties
        [ForeignKey("ShippingID")]  // Explicitly link the foreign key
        public ShippingInfo ShippingInfo { get; set; }

        [ForeignKey("DiscountCodeID")]  // Explicitly link the foreign key
        public DiscountCodes DiscountCodes { get; set; }

        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();
    }
}
