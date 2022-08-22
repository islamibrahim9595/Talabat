using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.BLL.Specifications.OrderSpecifications
{
    public class OrderWithItemsAndDeliveryMethodSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpecification(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);

            AddOrderByDescending(o => o.OrderDate);
           
        }

        public OrderWithItemsAndDeliveryMethodSpecification(int id, string buyerEmail) : 
            base(o => (o.BuyerEmail == buyerEmail && o.Id == id))
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
    }
}
