using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Password { get; set; }

    }
}
