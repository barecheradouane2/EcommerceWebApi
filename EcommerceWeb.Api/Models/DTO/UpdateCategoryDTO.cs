﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Api.Models.DTO
{
    public class UpdateCategoryDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "the length must be more than  3")]
        public string CategoryName { get; set; }
        public string Description { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

       
    }
}
