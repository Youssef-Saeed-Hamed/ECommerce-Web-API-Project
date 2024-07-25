using E_Commerce_Website_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Core.Interfaces.Repositories
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IGenericRepositry<TEntity , TKey> Repositry<TEntity , TKey>() 
            where TEntity : BaseEntity<TKey>;

        Task<int> CompleteAsync();
    }
}
