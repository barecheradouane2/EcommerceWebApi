using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ProductImages
    {
        [Key]
        public int ImageID { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; } 
        public string ImageUrl { get; set; }  

        public int ImageOrder { get; set; }

        [ForeignKey("ProductID")]
        public int ProductID { get; set; }

        //navigation property

      
        public ProductCatalog ProductCatalog { get; set; }

    }
}
