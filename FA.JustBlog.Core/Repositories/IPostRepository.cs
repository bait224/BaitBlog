using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FA.JustBlog.Core.Repositories
{
    public interface IPostRepository : IGerericRepository<Post, long>
    {
        Post FindPost(int year, int month, string urlSlug);
        IQueryable<Post> GetPublisedPosts();
        IQueryable<Post> GetUnpublisedPosts();
        IList<Post> GetLatestPost(int size);
        IList<Post> GetPostsByMonth(DateTime monthYear);
        int CountPostsForCategory(string category);
        IList<Post> GetPostsByCategory(string category);
        int CountPostsForTag(string tag);
        IList<Post> GetPostsByTag(string tag);
        IList<Post> GetMostViewedPost(int size);
        IList<Post> GetHighestPosts(int size);
        Post GetDetail(int year, int month, string title);
        void UpdatePublish(long id, bool status);
        IQueryable<Post> GetPublishedToFilter(Expression<Func<Post, bool>> filter = null,
            Expression<Func<Post, bool>> filterByUser = null,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,
            string includeProperties = "");
        IQueryable<Post> GetUnPublishedToFilter(Expression<Func<Post, bool>> filter = null,
            Expression<Func<Post, bool>> filterByUser = null,
            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null,
            string includeProperties = "");
    }
}
