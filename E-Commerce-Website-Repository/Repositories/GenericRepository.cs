using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Interfaces.Specifications;
using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Repository.Context;
using E_Commerce_Website_Repository.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Repositories
{
    internal class GenericRepository<TEntity, Tkey> : IGenericRepositry<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        private readonly DataContext context;

        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(TEntity entity) => await context.Set<TEntity>().AddAsync(entity);      
        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);       
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification)
                   => await ApplySpecifications(specification).ToListAsync();
        

        public async Task<TEntity> GetAsync(Tkey id) => (await context.Set<TEntity>().FindAsync(id))!;

        public async Task<int> GetProductCountWithSpecAsync(ISpecification<TEntity> specification)
        => await ApplySpecifications(specification).CountAsync();

        public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> specification)
                           => await ApplySpecifications(specification).FirstOrDefaultAsync();


        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);


        private IQueryable<TEntity> ApplySpecifications(ISpecification<TEntity> specification)
            => SpecificationEvautor<TEntity, Tkey>.
                BuildQuery(context.Set<TEntity>(), specification);
     }
}
