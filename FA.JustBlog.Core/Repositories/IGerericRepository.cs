using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.Repositories
{
    public interface IGerericRepository<TEntity,TId> where TEntity : Entity<TId> where TId: struct
    {
        TEntity Find(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);
        IQueryable<TEntity> GetAll();
        void DetachEntity(TEntity entity); 
        IQueryable<TEntity> GetAllToFilter(Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, bool>> filterByUser = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
    }
}
