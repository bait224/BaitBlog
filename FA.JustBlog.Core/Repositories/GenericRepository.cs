using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.Repositories
{
    public class GenericRepository<TEntity, TId> : IGerericRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        private DbContext _dbContext;
        private readonly DbSet<TEntity> dbSet;


        protected IDbFactory DbFactory { get; set; }
        public DbContext DbContext => _dbContext ?? (_dbContext = DbFactory.Init());

        public GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<TEntity>();
        }
        /// <summary>
        /// Implement Add method
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        /// <summary>
        /// Implement Delete method by entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
        /// <summary>
        /// Implement Delete method by entity id
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(TId id)
        {
            TEntity entity = Find(id);
            Delete(entity);
        }
        /// <summary>
        /// Implement Find method by entity id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(TId id)
        {
            return dbSet.Find(id);
        }
        /// <summary>
        /// Implement GetAll method 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }
        /// <summary>
        /// Implement Update method by entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<TEntity> GetAllToFilter(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, bool>> filterByUser = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (filterByUser != null)
            {
                query = query.Where(filterByUser);
            }
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return orderBy != null ? orderBy(query) : query;
        }

        public void DetachEntity(TEntity entity)
        {
            var localEntity = Find(entity.Id);
            DbContext.Entry(localEntity).State = EntityState.Detached;
        }
    }
}
