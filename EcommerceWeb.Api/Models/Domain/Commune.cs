using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Commune
    {

        public int CommuneID { get; set; }
        public string CommuneName { get; set; }

        public int WilayaID { get; set; }

        // navigation property

        [ForeignKey("WilayaID")]
        public Wilaya Wilaya { get; set; }
    }
}
