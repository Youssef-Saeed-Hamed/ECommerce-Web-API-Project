using E_Commerce_Website_Core.Interfaces.Repositories;
using E_Commerce_Website_Core.Models;
using E_Commerce_Website_Repository.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Website_Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Hashtable _repositories;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
       

        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
        

        public IGenericRepositry<TEntity, TKey> Repositry<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var TypeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(TypeName))
                return (_repositories[TypeName] as GenericRepository<TEntity , TKey>)!;
            var repo = new GenericRepository<TEntity , TKey>(_context);
            _repositories.Add(TypeName, repo);
            return repo;                            
        }
    }
}


