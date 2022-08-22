using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.BLL.Specifications.OrderSpecifications
{
    public class OrderWithItemsByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsByPaymentIntentIdSpecification(string paymentIntentId) : base(O => O.PaymentIntentId == paymentIntentId)
        {
            AddIncludes(o => o.OrderItems);
        }
    }
}
