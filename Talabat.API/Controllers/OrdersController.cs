using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.API.DTOS;
using Talabat.API.Errors;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Order_Aggergate;

namespace Talabat.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO ordeDTO)
        {
            var buyerEmail =  User.FindFirstValue(ClaimTypes.Email);

            var orderAddress = _mapper.Map<AddressDTO, Address>(ordeDTO.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(buyerEmail, orderAddress, ordeDTO.DeliveryMethodId, ordeDTO.BasketId);

            if (order == null) return BadRequest(new ApiResponse(400, "An Error Occured During Creating The Order"));

            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderService.GetOrdersForUser(buyerEmail);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(orders));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForUser(id, buyerEmail);

            return Ok(_mapper.Map<Order, OrderToReturnDTO>(order));
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {

            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}
