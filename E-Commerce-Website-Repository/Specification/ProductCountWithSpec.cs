using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Specification
{
    public class ProductCountWithSpec : BaseSpecification<Product>
    {
        public ProductCountWithSpec(ProductSpecificationParameters parameters) 
            : base(product =>
            (!parameters.BrandId.HasValue || product.Id == parameters.BrandId.Value) &&
            (!parameters.TypeId.HasValue || product.Id == parameters.TypeId.Value)&&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search)))

        {

        }
    }
}
