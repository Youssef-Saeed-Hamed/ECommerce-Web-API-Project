using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Specifications
{

    public class ProductSpecificationParameters
    {
        private const int MAXPAGESIZE = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public SortingEnum? Sorting { get; set; }

        public int PageIndex { get; set; } = 1;
        private int _PageSize = 5;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value; }
        }

        private string? SearchValue;
        public string ? Search
        {
            get => SearchValue;
            set => SearchValue = value?.Trim().ToLower();
        }


    }
}
