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
    public class PostService : GenericService<Post, long>, IPostService
    {
        private readonly IPostRepository repository;
        private readonly ICategoryRepository categoryRepository;
        public PostService(IUnitOfWork _unitOfWork, IPostRepository _repository, ICategoryRepository _categoryRepository) : base(_repository, _unitOfWork)
        {
            repository = _repository;
            categoryRepository = _categoryRepository;
        }

        public override ResponseModel Add(Post entity)
        {
            using( var dbTransaction = _unitOfWork.DbContext.Database.BeginTransaction())
            {
                try
                {
                    if (entity != null)
                    {
                        if (entity.CategoryId != null && entity.CategoryId != 0)
                        {
                            var category = categoryRepository.Find(entity.CategoryId ?? 0);
                            if (category == null)
                            {
                                return Common.ResponseData.Response(Status: 302, Message: "Can not find category");
                            }
                            entity.Category = category;
                        }

                        if (entity.Category.Id == 0)
                        {
                            Category newCatefory = entity.Category;
                            categoryRepository.Add(newCatefory);
                            _unitOfWork.Commit();


                            entity.CategoryId = newCatefory.Id;
                            entity.Category.Id = newCatefory.Id; 
                        }


                        entity.PostedOn = DateTime.UtcNow;

                        entity.ModifiedDate = entity.PostedOn;

                        repository.Add(entity);
                        _unitOfWork.Commit();

                        dbTransaction.Commit();
                        
                        return Common.ResponseData.Response(Status: 200, Message: "Create success", Data: entity);
                    }
                    else
                    {
                        return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
                    }
                }
                catch(Exception ex)
                {
                    dbTransaction.Rollback();
                    return ResponseData.Response(Status: 302, Message: "Error: " + ex.Message, Data: null);
                }
            }
            
        }

        public override ResponseModel Update(Post entity)
        {
            if (entity != null)
            {
                if (entity.CategoryId != null)
                {
                    var category = categoryRepository.Find(entity.CategoryId ?? 0);
                    if (category == null)
                    {
                        return Common.ResponseData.Response(Status: 302, Message: "Can not find category");
                    }
                }

            }
            return base.Update(entity);
        }
        public ResponseModel CountPostsForCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid", Data: 0);
            }
            else
            {
                try
                {
                    int countResult = repository.CountPostsForCategory(category);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: countResult);

                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message, Data: 0);
                }
            }

        }

        public ResponseModel CountPostsForTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid", Data: 0);
            }
            else
            {
                try
                {
                    int countResult = repository.CountPostsForTag(tag);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: countResult);

                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message, Data: 0);
                }
            }
        }

        public ResponseModel FindPost(int year, int month, string urlSlug)
        {
            try
            {
                var findResult = repository.FindPost(year, month, urlSlug);
                return Common.ResponseData.Response(Status: 200, Message: "Success", Data: findResult);
            }
            catch (Exception ex)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
            }

        }

        public ResponseModel GetHighestPosts(int size)
        {
            if (size <= 0)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetHighestPosts(size);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetLatestPost(int size)
        {
            if (size <= 0)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetLatestPost(size);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetMostViewedPost(int size)
        {
            if (size <= 0)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetMostViewedPost(size);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetPostsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetPostsByCategory(category);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetPostsByMonth(DateTime monthYear)
        {
            if (string.IsNullOrEmpty(monthYear.ToString()))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetPostsByMonth(monthYear);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetPostsByTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetPostsByTag(tag);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetPublisedPosts()
        {
            try
            {
                var getResult = repository.GetPublisedPosts().ToList();
                return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
            }
            catch (Exception ex)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
            }
        }

        public ResponseModel GetUnpublisedPosts()
        {
            try
            {
                var getResult = repository.GetUnpublisedPosts().ToList();
                return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
            }
            catch (Exception ex)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
            }
        }

        public ResponseModel GetDetail(int year, int month, string title)
        {
            if (year <= 0 || (month < 0 && month > 12) || string.IsNullOrEmpty(title))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var result = repository.GetDetail(year, month, title);
                    _unitOfWork.Commit();
                    return Common.ResponseData.Response(Status: 200, Data: result, Message: "Update View Success");
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Update View Fail - Error: " + ex.Message);
                }
            }
        }

        public async Task<ResponseModel> GetPublishedAsyncPagination(Expression<Func<Post, bool>> filter = null, Expression<Func<Post, bool>> filterByUser = null, Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null, string includeProperties = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var query = repository.GetPublishedToFilter(filter: filter, filterByUser: filterByUser, includeProperties: includeProperties);
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                var result = await PaginationList<Post>.CreateAsync(query.AsNoTracking(), page, pageSize);
                return ResponseData.Response(Status: 200, Data: result, Message: "Success");
            }
            catch (Exception ex)
            {
                return ResponseData.Response(Status: 302, Data: null, Message: "Error: " + ex.Message);
            }
        }

        public async Task<ResponseModel> GetUnPublishedAsyncPagination(Expression<Func<Post, bool>> filter = null, Expression<Func<Post, bool>> filterByUser = null, Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null, string includeProperties = "", int page = 1, int pageSize = 10)
        {
            try
            {
                var query = repository.GetUnPublishedToFilter(filter: filter, filterByUser: filterByUser, includeProperties: includeProperties);
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                var result = await PaginationList<Post>.CreateAsync(query.AsNoTracking(), page, pageSize);
                return ResponseData.Response(Status: 200, Data: result, Message: "Success");
            }
            catch (Exception ex)
            {
                return ResponseData.Response(Status: 302, Data: null, Message: "Error: " + ex.Message);
            }
        }

        public ResponseModel UpdatePublish(long id, bool status)
        {
            if (id <= 0)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }

            try
            {
                repository.UpdatePublish(id, status);
                _unitOfWork.Commit();
                return Common.ResponseData.Response(Status: 200, Message: "Update Success");
            }
            catch (Exception ex)
            {
                return ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
            }
        }
    }
}
