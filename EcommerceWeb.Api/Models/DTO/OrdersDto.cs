﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // For JsonIgnore

namespace EcommerceWeb.Api.Models.DTO
{
    public class OrdersDto
    {
        [Key]
        public int OrderID { get; set; }
        [JsonIgnore]
        public DateTime OrderDate { get; set; }

        public string CurrentOrderDate
        {
            get
            {
                return OrderDate.ToString("yyyy/MM/dd");
            }
        }
        public decimal TotalAmount { get; set; }

        public string OrderStatusDescription
        {
            get
            {
                return OrderStatus switch
                {
                    0 => "Pending",
                    1 => "Confirm",
                    2 => "Return",
                    _ => "Unknown" // Default value for unexpected statuses.
                };
            }
        }

        [JsonIgnore] // Hides this property during JSON serialization
        public int OrderStatus { get; set; }

        public string FullName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Wilaya { get; set; }
        public string Commune { get; set; }
        public string OrderAddress { get; set; }

        [JsonIgnore]
        public int DiscountCodeID { get; set; }

        [JsonIgnore]
        public int ShippingID { get; set; }

        [JsonIgnore]
        public int ShippingStatus { get; set; }


        public List <OrderItemsDTO > OrderItems { get; set; }

        // Computed property to map OrderStatus to a string representation.
      
    }
}