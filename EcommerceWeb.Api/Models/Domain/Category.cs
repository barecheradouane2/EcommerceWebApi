using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Category
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public string Description { get; set; }


        public ICollection<ProductCatalog> ProductCatalog { get; set; } = new List<ProductCatalog>();
    }
}
