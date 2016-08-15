using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BikeTracker.Data;

namespace BikeTracker.Repositories
{
    public abstract class EFRepository<T> where T : class
    {
        protected readonly BikeTrackerDbContext _dbContext = new BikeTrackerDbContext();

        protected T GetBy(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().SingleOrDefault(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Save(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbContext.Set<T>().Add(entity);

            _dbContext.SaveChanges();
        }

        protected bool DeleteBy(Expression<Func<T, bool>> expression)
        {
            var entity = _dbContext.Set<T>().SingleOrDefault(expression);
            if (entity == null)
                return false;
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }
    }
}