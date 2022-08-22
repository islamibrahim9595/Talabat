using AutoMapper;
using Talabat.API.DTOS;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Identity;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(source => source.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(source => source.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlResolver>());


            CreateMap<ProductBrand, ProductBrandToReturnDTO>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));

            CreateMap<ProductType, ProductTypeToReturnDTO>()
               .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));


            CreateMap<DAL.Entities.Identity.Address, AddressDTO>().ReverseMap();

            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<BasketItemDTO, BasketItem>();

            CreateMap<AddressDTO, DAL.Entities.Order_Aggergate.Address>();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, OI => OI.MapFrom(s => s.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName, OI => OI.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, OI => OI.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, OI => OI.MapFrom<OrderItemUrlResolver>());
                
                
        }
    }
}
