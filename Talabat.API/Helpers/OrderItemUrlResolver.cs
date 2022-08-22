using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.API.DTOS;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.API.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return $"{_configuration["ApiUrl"]}{source.ItemOrdered.PictureUrl}";
            }

            return null;
            
        }
    }
}
