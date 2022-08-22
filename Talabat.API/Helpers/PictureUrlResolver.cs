using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.API.DTOS;
using Talabat.DAL.Entities;

namespace Talabat.API.Helpers
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiUrl"]}{source.PictureUrl}";

            return null;
        }
    }
}
