using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ProductImages
    {
        [Key]
        public int ImageID { get; set; }

        public string ImageUrl { get; set; }

        public int ImageOrder { get; set; }

        public int ProductID { get; set; }

        //navigation property

        [ForeignKey("ProductID")]
        public ProductCatalog ProductCatalog { get; set; }

    }
}
