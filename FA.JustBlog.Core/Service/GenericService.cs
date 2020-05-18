using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Service
{
    public class GenericService<TEntity, TId> : IGenericService<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        private readonly IGerericRepository<TEntity, TId> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        public GenericService(IGerericRepository<TEntity, TId> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public virtual ResponseModel Add(TEntity entity)
        {
            if (entity == null)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            _repository.Add(entity);
            int result = _unitOfWork.Commit();
            return Common.ResponseData.Response(Status: 200, Message: "Success", Data: result);
        }

        public ResponseModel Delete(TEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    _repository.Delete(entity);
                    _unitOfWork.Commit();
                    return Common.ResponseData.Response(Status: 200, Message: "Delete Success");
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Delete Fail - Error: " + ex.Message);
                }

            }
            else
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
        }

        public ResponseModel Delete(TId id)
        {
            try
            {
                _repository.Delete(id);
                _unitOfWork.Commit();
                return Common.ResponseData.Response(Status: 200, Message: "Delete Success");
            }
            catch (Exception ex)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Delete Fail - Error: " + ex.Message);
            }
        }

        public ResponseModel Find(TId id)
        {
            return Common.ResponseData.Response(Status: 200, Message: "Success", Data: _repository.Find(id));
        }

        public ResponseModel GetAll()
        {

            return Common.ResponseData.Response(Status: 200, Message: "Success", Data: _repository.GetAll().ToList());
        }

        public virtual ResponseModel Update(TEntity entity)
        {
            if (entity != null)
            {
                try
                {
                    _repository.Update(entity);
                    _unitOfWork.Commit();
                    return Common.ResponseData.Response(Status: 200, Message: "Update Success");
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Update Fail - Error: " + ex.Message);
                }

            }
            else
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
        }

        public async Task<ResponseModel> GetAllAsyncPagination(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, bool>> filterByUser = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var query = _repository.GetAllToFilter(filter: filter, filterByUser: filterByUser, includeProperties: includeProperties);
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                var result = await PaginationList<TEntity>.CreateAsync(query.AsNoTracking(), page, pageSize);
                return ResponseData.Response(Status: 200, Data: result, Message: "Success");
            }
            catch (Exception ex)
            {
                return ResponseData.Response(Status: 302, Data: null, Message: "Error: " + ex.Message);
                throw;
            }
        }

        public void DetachEntity(TEntity entity)
        {
            _repository.DetachEntity(entity);
        }
    }
}
