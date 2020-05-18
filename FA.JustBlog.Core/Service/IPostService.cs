using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Service
{
    public interface IPostService : IGenericService<Post, long>
    {
        ResponseModel FindPost(int year, int month, string urlSlug);
        ResponseModel GetPublisedPosts();
        ResponseModel GetUnpublisedPosts();
        ResponseModel GetLatestPost(int size);
        ResponseModel GetPostsByMonth(DateTime monthYear);
        ResponseModel CountPostsForCategory(string category);
        ResponseModel GetPostsByCategory(string category);
        ResponseModel CountPostsForTag(string tag);
        ResponseModel GetPostsByTag(string tag);
        ResponseModel GetMostViewedPost(int size);
        ResponseModel GetHighestPosts(int size);
        ResponseModel GetDetail(int year, int month, string title);
        ResponseModel UpdatePublish(long id, bool status);
        Task<ResponseModel> GetPublishedAsyncPagination(Expression<Func<Post, bool>> filter = null,
            Expression<Func<Post, bool>> filterByUser = null,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,
            string includeProperties = "", int page = 1, int pageSize = 10);
        Task<ResponseModel> GetUnPublishedAsyncPagination(Expression<Func<Post, bool>> filter = null,
            Expression<Func<Post, bool>> filterByUser = null,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,
            string includeProperties = "", int page = 1, int pageSize = 10);
    }
}
