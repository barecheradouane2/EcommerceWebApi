using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ProductColorVariant
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductColorVariantID { get; set; }

        public string Color { get; set; } = string.Empty;

        // Optional: You can include quantity per color
        public int Quantity { get; set; } = 0;

        // Foreign key to ProductSize
      

        [ForeignKey("ProductSizeID")]

        public int ProductSizeID { get; set; }


        public ProductSize ProductSize { get; set; }


    }
}
