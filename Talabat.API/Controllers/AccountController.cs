using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.API.DTOS;
using Talabat.API.Errors;
using Talabat.API.Extentions;
using Talabat.BLL.Interfaces;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, 
            ITokenService tokenService, SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }


        [HttpPost("register")]

        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(CheckEmailExists(registerDTO.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "This Email is already in use" } });


            var user = new AppUser()
            {
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.Email.Split("@")[0],
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                Address = new Address()
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    Country = registerDTO.Country,
                    City = registerDTO.City,
                    Street = registerDTO.Street
                }
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)

            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDTO()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
        }

        [Authorize]
        [HttpPut("address")]

        public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO newAddress)
        {
            var user = await _userManager.FindUserWithAddressByEmailAsync(User);

            user.Address = _mapper.Map<AddressDTO, Address>(newAddress);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse() { Errors = new[] { "an Error Occured With Updating User Address" } });

            return Ok(newAddress); 

        }


        [HttpGet("CheckEmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists([FromQuery]string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
    
}