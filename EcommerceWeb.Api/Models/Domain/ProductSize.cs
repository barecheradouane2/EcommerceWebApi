
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class ProductSize
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductSizeID { get; set; }

        public string Size { get; set; } = string.Empty;

        // You can add quantity per size if needed
        public int Quantity { get; set; } = 0;

        // Foreign Key
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public ProductCatalog Product { get; set; }


        public ICollection<ProductColorVariant> ProductColorVariant { get; set; }
    }
}
