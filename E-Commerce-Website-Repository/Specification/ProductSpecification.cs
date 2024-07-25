using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {

        public ProductSpecification(ProductSpecificationParameters specs) :base(product=>
            (!specs.BrandId.HasValue || product.Id == specs.BrandId.Value)&&
            (!specs.TypeId.HasValue || product.Id == specs.TypeId.Value) && 
            (string.IsNullOrWhiteSpace(specs.Search) || product.Name.ToLower().Contains(specs.Search))
        )
        {
            Includes.Add(product => product.Brand);
            Includes.Add(product => product.ProductType);

            ApplyPaggination(specs.PageSize, specs.PageIndex);

            if(specs.Sorting is not null)
            {
                switch(specs.Sorting)
                {
                    case SortingEnum.NameAsc:
                        OrderAsc = x => x.Name;
                        break;
                    case SortingEnum.NameDesc:
                        OrderDesc = x => x.Name;
                        break;
                    case SortingEnum.PriceAsc:
                        OrderAsc = x => x.Price;
                        break;
                    case SortingEnum.PriceDesc:
                        OrderDesc = x => x.Price;
                        break;
                    default:
                        OrderAsc = x => x.Name;
                        break;
                }
            }
        }

        public ProductSpecification(int Id) : base(product=> product.Id == Id)
        {
            Includes.Add(product => product.Brand);
            Includes.Add(product => product.ProductType);
        }
    }
}
