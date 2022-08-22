﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTOS
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity Must be greater than 0")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
