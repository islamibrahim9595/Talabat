using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specifications.OrderSpecifications;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMetodRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OrderService(IBasketRepository basketRepository,
             //IGenericRepository<Product> productRepo,
             //IGenericRepository<DeliveryMethod> deliveryMetodRepo, 
             //IGenericRepository<Order> orderRepo
             IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productRepo = productRepo;
            //_deliveryMetodRepo = deliveryMetodRepo;
            //_orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, Address ShipToAddress, int deliveryMethodId, string basketId)
        {
            //1.Get Basket From Baskets Repo

            var basket = await _basketRepository.GetCustomerBasket(basketId);

            //2. Get Selected Items at Basket From Products Repo

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetById(item.Id);

                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                orderItems.Add(orderItem);

            }

            //3. Get Delivery Method From DeliveryMethods Repo 

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(deliveryMethodId);

            //4. calculate Subtotal

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            //..check if order is exited or not

            var spec = new OrderWithItemsByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if(existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }

            //5. Create Order

            var order = new Order(buyerEmail, ShipToAddress, orderItems, deliveryMethod, subtotal, basket.PaymentIntentId);

            await _unitOfWork.Repository<Order>().Add(order);

            //6. Save To Database

            int result = await _unitOfWork.Complete();
            if(result <= 0) return null;

            return order;
        }


        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }


        public async Task<Order> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(orderId ,buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUser(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec); 
        }
    }
}
