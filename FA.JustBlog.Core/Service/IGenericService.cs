using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Service
{
    public interface IGenericService<TEntity, TId> where TEntity : IEntity<TId> where TId:struct
    {
        ResponseModel Find(TId id);
        ResponseModel Add(TEntity entity);
        ResponseModel Update(TEntity entity);
        ResponseModel Delete(TEntity entity);
        ResponseModel Delete(TId id);
        ResponseModel GetAll();
        void DetachEntity(TEntity entity);
        Task<ResponseModel> GetAllAsyncPagination(Expression<Func<TEntity, bool>> filter = null, 
            Expression<Func<TEntity, bool>> filterByUSer = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int page = 1, int pageSize = 10);
    }
}
