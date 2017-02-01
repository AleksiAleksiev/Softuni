namespace PhotographyWorkshops.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Find(int id);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);

        TEntity FirstOrDefault();

        IQueryable<TEntity> GetAll();

    }
}
