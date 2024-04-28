using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOS;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
   
    public class ProductController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _genericRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> genericRepo, IGenericRepository<ProductBrand> brandRepo , IGenericRepository<ProductCategory> categoryRepo, IMapper mapper)
        {
           _genericRepo = genericRepo;
            _brandRepo = brandRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ProductToReturnDTOs),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecification(specParams);
            var Products = await _genericRepo.GetAllSpecAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTOs>>(Products);
            var countspec = new ProductWithFilterationForCountSpec(specParams);
            var count = await _genericRepo.GetCountAsync(countspec);
            return Ok(new Pagination<ProductToReturnDTOs>(specParams.PageIndex, count,specParams.PageSize,data) );
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDTOs), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecification(id);
            var Products = await _genericRepo.GetSpecAsync(spec);

            if(Products == null)
            {
                return NotFound(/*new { Message = "Not Found", StatusCode = "404" }*/  new APIResponse(404));
            }

            return Ok(_mapper.Map<Product,ProductToReturnDTOs>(Products)); //200
        }



        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetAllBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }


        [HttpGet("categories")]
        public async Task<ActionResult<ProductBrand>> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }

    }
}
