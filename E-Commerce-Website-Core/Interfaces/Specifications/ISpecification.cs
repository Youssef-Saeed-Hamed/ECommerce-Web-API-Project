using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Specifications
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T , object>>> Includes { get; }
        public Expression<Func<T , object>> OrderAsc { get; }
        public Expression<Func<T , object>> OrderDesc { get; }

        public int Skip { get; }
        public int Take { get; }    
        public bool IsPaginated { get; }

    }
}
