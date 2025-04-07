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

        public string Description { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;


        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public string ImagePath { get; set; } = string.Empty;


        public ICollection<ProductCatalog> ProductCatalog { get; set; } = new List<ProductCatalog>();
    }
}
