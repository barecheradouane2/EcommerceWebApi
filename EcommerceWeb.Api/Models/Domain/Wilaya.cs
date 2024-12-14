using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Wilaya
    {
        [Key]
        public int WilayaID { get; set; }

        public string WilayaName { get; set; }
    
    }
}
