namespace EcommerceWeb.Api.Models.Domain
{
    public class DiscountCodes
    {

        public int ID { get; set; }

        public string Code { get; set; }

        public int DiscountPercentage { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal MinPurchaseAmount { get; set; }
    }
}
