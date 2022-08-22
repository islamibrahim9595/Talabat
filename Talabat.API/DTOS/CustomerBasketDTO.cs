using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Talabat.DAL.Entities;

namespace Talabat.API.DTOS
{
    public class CustomerBasketDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDTO> Items { get; set; } = new List<BasketItemDTO>();

        public int? DeliveryMethodId { get; set; }

        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
