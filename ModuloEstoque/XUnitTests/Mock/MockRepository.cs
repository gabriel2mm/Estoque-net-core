using System;
using System.Collections.Generic;
using System.Linq;
using Stock.Domain.Interfaces;

namespace Stock.XUnitTests.Mock
{
    public class MockRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private bool isDisposed;
        protected readonly ICollection<TEntity> context = new List<TEntity>();

        public virtual IQueryable<TEntity> GetAll()
        {
            return context.AsQueryable();
        }

        public virtual IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return GetAll().Where(predicate).AsQueryable();
        }

        public virtual TEntity Find(params object[] key)
        {
            return context.First();
        }

        public virtual void Update(TEntity obj)
        {
            
        }

        public virtual void SaveAll()
        {
          
        }

        public virtual void Add(TEntity obj)
        {
            context.Add(obj);
        }

        public virtual void Delete(Func<TEntity, bool> predicate)
        {
            TEntity entity = context.Where(predicate).FirstOrDefault();
            context.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {                
                GC.Collect();
            }

            isDisposed = true;
        }

        ~MockRepository()
        {
            Dispose(false);
        }
    }
}
