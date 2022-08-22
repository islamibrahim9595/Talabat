using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, Address ShipToAddress, int deliveryMethodId, String basketId);

        Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail);
        Task<Order> GetOrderByIdForUser(int orderId, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}
