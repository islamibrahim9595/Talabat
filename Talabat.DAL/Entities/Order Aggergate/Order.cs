using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.DAL.Entities.Order_Aggergate
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }

        public Order(string buyerEmail, Address address, List<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subtotal,
            string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = address;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShipToAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal Subtotal { get; set; }

        public decimal GetTotal()
            => Subtotal + DeliveryMethod.Cost;

    }
}
