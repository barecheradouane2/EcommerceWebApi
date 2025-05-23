﻿using EcommerceWeb.Api.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.DTO
{
    public class AddOrderRequestDTO
    {

        [Required]

        public List<AddOrderItemsDTO> OrderItems { get; set; }


        //public DateTime OrderDate { get; set; }=DateTime.Now;

        //public decimal TotalPrice { get; set; } = 0;

        //public int OrderStatus { get; set; } = 0;

        public string FullName { get; set; }

        public string TelephoneNumber { get; set; }

        public string OrderAddress { get; set; }

        public int WilayaID { get; set; }

        public int CommuneID { get; set; }

        //public int DiscountCodeID { get; set; }

        public int ShippingID { get; set; }

        public int ShippingStatus { get; set; } = 0;









    }
}
