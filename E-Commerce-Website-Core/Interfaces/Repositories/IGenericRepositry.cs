using E_Commerce_Website_Core.Interfaces.Specifications;
using E_Commerce_Website_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Repositories
{
    public interface IGenericRepositry<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task <IEnumerable<TEntity>> GetAllAsync();
        Task <IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification);
        Task <int> GetProductCountWithSpecAsync(ISpecification<TEntity> specification);
        Task <TEntity> GetAsync(TKey id);
        Task <TEntity> GetWithSpecAsync(ISpecification<TEntity> specification);
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);

    }
}
