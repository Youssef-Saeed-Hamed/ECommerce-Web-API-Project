using AutoMapper;
using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Services;
using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Specifications;
using E_Commerce_Website_Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<IEnumerable<BrandAndTypesToReturnDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.Repositry<ProductBrand , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandAndTypesToReturnDto>>(Brands);
        }

        public async Task<PaginationResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var spec = new ProductSpecification(parameters);
            var products = await _unitOfWork.Repositry<Product, int>().GetAllWithSpecAsync(spec);
            var CountSpec = new ProductCountWithSpec(parameters);
            var count = await _unitOfWork.Repositry<Product, int>().GetProductCountWithSpecAsync(CountSpec);
            var productMapped =  _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return new PaginationResultDto<ProductToReturnDto>
            {
                Data = productMapped,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                TotalCount = count,
            };
        }

        public async Task<IEnumerable<BrandAndTypesToReturnDto>> GetAllTypesAsync()
        {
           var Types = await _unitOfWork.Repositry<ProductType , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<BrandAndTypesToReturnDto>>(Types);
        }

       
        async Task<ProductToReturnDto> IProductServices.GetProductAsync(int id)
        {
            var spec = new ProductSpecification(id);
            var product = await _unitOfWork.Repositry<Product, int>().GetWithSpecAsync(spec);
            return _mapper.Map<ProductToReturnDto>(product);
        }
    }
}
