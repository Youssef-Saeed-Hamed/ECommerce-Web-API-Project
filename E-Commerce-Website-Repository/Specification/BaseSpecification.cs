using E_Commerce_Website_Core.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public List<Expression<Func<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>> OrderAsc { get; protected set; }

        public Expression<Func<T, object>> OrderDesc { get; protected set; }

        public int Skip { get; protected set; }
        public int Take { get; protected set; }
        protected void ApplyPaggination (int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }


        public bool IsPaginated { get; protected set; }
    }
}
