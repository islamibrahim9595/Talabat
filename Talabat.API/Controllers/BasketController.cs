using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.API.DTOS;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers
{

    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket = await _basketRepository.GetCustomerBasket(basketId);

            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {

            var mappedBasket = _mapper.Map<CustomerBasketDTO, CustomerBasket>(basket);

            var CustomerBasket = await _basketRepository.UpdateCustomerBasket(mappedBasket);

            return Ok(CustomerBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            return await _basketRepository.DeleteCustomerBasket(basketId);
        }
    }
}

