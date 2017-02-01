namespace PhotographyWorkshops.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using PhotographyWorkshops.Data.Interfaces;

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> set;

        public Repository(DbSet<TEntity> entities)
        {
            this.set = entities;
        }

        public void Add(TEntity entity)
        {
            this.set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.set.AddRange(entities);
        }

        public TEntity Find(int id)
        {
            return this.set.Find(id);
        }

        public TEntity FirstOrDefault()
        {
            return this.set.FirstOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            return this.set.FirstOrDefault(expression);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.set;
        }

        public void Remove(TEntity entity)
        {
            this.set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.set.RemoveRange(entities);
        }
    }
}
