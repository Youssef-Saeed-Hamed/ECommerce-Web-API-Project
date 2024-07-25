using E_Commerce_Website_Core.DataTransferObjects;
using E_Commerce_Website_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Services
{
    public interface IProductServices
    {
        Task<PaginationResultDto<ProductToReturnDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);
        Task<ProductToReturnDto> GetProductAsync(int id);
        Task<IEnumerable<BrandAndTypesToReturnDto>> GetAllBrandsAsync();
        Task<IEnumerable<BrandAndTypesToReturnDto>> GetAllTypesAsync();
    }
}
