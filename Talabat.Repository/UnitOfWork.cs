using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        //private  Dictionary<string, GenericRepository<BaseEntity>> _repositories;
        //Instead of dictionary we can make HashTable so we will not need explicit casting
        private Hashtable _repositories;


        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            //_repositories = new Dictionary<string, GenericRepository<BaseEntity>>();    
            _repositories = new Hashtable();
        }

 



        //To Create Repo per Request
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_storeContext) ;
                _repositories.Add(key, repository) ;
            }

            return (IGenericRepository<TEntity>)_repositories[key] ;
        }



        public Task<int> CompleteAsync()
          => _storeContext.SaveChangesAsync();
        public ValueTask DisposeAsync() => _storeContext.DisposeAsync();

    }
}
