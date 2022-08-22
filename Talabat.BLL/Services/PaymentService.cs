using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggergate;
using Product = Talabat.DAL.Entities.Product;

namespace Talabat.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepo,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _basketRepo.GetCustomerBasket(basketId);

            if (basket == null) return null;

            var shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetById(basket.DeliveryMethodId.Value);

                shippingPrice = deliveryMethod.Cost;
            }
           
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetById(item.Id);

                if(item.Price != product.Price)
                    item.Price = product.Price;

            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId)) // incase of create PaymentIntent 
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(basket.Items.Sum(i => i.Quantity * (i.Price * 100))) + (long)(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                intent = await service.CreateAsync(options);

                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else //incase of update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)(shippingPrice * 100)
                };

                await service.UpdateAsync(basket.PaymentIntentId, options);

            }

            basket.ShippingPrice = shippingPrice;

            await _basketRepo.UpdateCustomerBasket(basket);

            return basket;
        }
    }
}
