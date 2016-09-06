using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BikeTracker.Data;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public abstract class EFRepository<T> where T : Entity
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
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
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