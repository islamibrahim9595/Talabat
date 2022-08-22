using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.API.DTOS;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specifications;
using Talabat.BLL.Specifications.ProductSpecification;
using Talabat.DAL.Entities;

namespace Talabat.API.Controllers
{
    
    public class ProductsController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        //{
        //    var products = await _productRepo.GetAllAsync();

        //    return Ok(products);
        //}


        [HttpGet]

        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts2([FromQuery]ProductSpecParams productSpecParams)
        {
            var products = await _productRepo.GetAllWithSpecAsync(new ProductsWithTypesAndBrandsSpecification(productSpecParams));
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(products);
            var count = await _productRepo.CountAsync(new ProductWithFiltersCountSpecification(productSpecParams));
            
            return Ok(new Pagination<ProductToReturnDTO>(productSpecParams.PageIndex, productSpecParams.pageSize, count, Data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
        {
            var product = await _productRepo.GetEntityWithSpecAsync(new ProductsWithTypesAndBrandsSpecification(id));


            if(product == null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDTO>(product));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct2(int id)
        //{
        //    var product = await _productRepo.GetById(id);

        //    return Ok(product);
        //}

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrandToReturnDTO>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();

            return Ok(_mapper.Map< IReadOnlyList<ProductBrand>, IReadOnlyList<ProductBrandToReturnDTO>>(brands));
        }

        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductTypeToReturnDTO>>> GetTypes()
        {
            var types = await _typeRepo.GetAllAsync();

            return Ok(_mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<ProductTypeToReturnDTO>>(types));
        }
    }

}
