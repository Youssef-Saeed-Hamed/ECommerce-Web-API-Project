using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Specifications;
using E_Commerce_Website_Persentation.Errors;
using E_Commerce_Website_Persentation.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Website_Persentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices) {
            _productServices = productServices;
        }
        [Authorize]
        [HttpGet]
        [Cash(60)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecificationParameters parameters)
            => Ok(await _productServices.GetAllProductsAsync(parameters));
         [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product  = await _productServices.GetProductAsync(id);
            return product is not null ? Ok(product) : NotFound(new APIResponse(404 , $"Product With Id {id} is Not Found"));
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandAndTypesToReturnDto>>> GetBrands()
            => Ok(await _productServices.GetAllBrandsAsync());
         [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<BrandAndTypesToReturnDto>>> GetTypes()
            => Ok(await _productServices.GetAllTypesAsync());

    }
}

