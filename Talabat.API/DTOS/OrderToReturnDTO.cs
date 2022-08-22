using System;
using System.Collections.Generic;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.API.DTOS
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 

        public string Status { get; set; } 
        public Address ShipToAddress { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public string DeliveryMethod { get; set; }

        public decimal DeliveryMethodCost { get; set; }
        public int PaymentIntentId { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
    }
}
