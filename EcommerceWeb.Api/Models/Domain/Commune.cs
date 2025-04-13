using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Api.Models.Domain
{
    public class Commune
    {

        public int CommuneID { get; set; }
        public string CommuneName { get; set; }

      

        // navigation property

        [ForeignKey("WilayaID")]

        public int WilayaID { get; set; }


        public Wilaya Wilaya { get; set; }
    }
}
