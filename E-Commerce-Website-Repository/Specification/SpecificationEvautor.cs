using E_Commerce_Website_Core.Interfaces.Specifications;
using E_Commerce_Website_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Specification
{
    public class SpecificationEvautor<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> BuildQuery(IQueryable<TEntity> inputQuery , ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            if(specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            //foreach (var item in specification.Includes)
            //    query = query.Include(item);

            if(specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            query = specification.Includes.Aggregate(query , (currentQuery, expression) => 
            currentQuery.Include(expression));

            if(specification.OrderAsc is not null)            
                query = query.OrderBy(specification.OrderAsc);

            if (specification.OrderDesc is not null)
                query = query.OrderByDescending(specification.OrderDesc);
            

            return query;
        }
    }
}
