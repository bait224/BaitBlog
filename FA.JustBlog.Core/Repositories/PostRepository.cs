using System;
using System.Collections.Generic;
using FA.JustBlog.Core.Models;
using System.Linq;
using FA.JustBlog.Core.Infrastructure;
using System.Linq.Expressions;
using System.Data.Entity;

namespace FA.JustBlog.Core.Repositories
{
    public class PostRepository : GenericRepository<Post, long>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactorry) : base(dbFactorry)
        {
        }

        public int CountPostsForCategory(string category)
        {
            return DbContext.Set<Post>().Count(p => p.Category.Name.Contains(category));
        }

        public int CountPostsForTag(string tag)
        {
            return DbContext.Set<Post>().Count(p => p.Tags.Any( t => t.Name.Contains(tag)));
        }

        public Post FindPost(int year, int month, string urlSlug)
        {
            return DbContext.Set<Post>().FirstOrDefault(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug == urlSlug);
        }

        public Post GetDetail(int year, int month, string title)
        {
            using (var dbTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var post = FindPost(year, month, title);
                    post.UpdateView();

                    dbTransaction.Commit();
                    return post;
                }
                catch
                {
                    dbTransaction.Rollback();
                    return null;
                }
            }
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return DbContext.Set<Post>().OrderByDescending(p => p.Rate).Take(size).ToList();
        }

        public IList<Post> GetLatestPost(int size)
        {
            return DbContext.Set<Post>().OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }

        public IList<Post> GetMostViewedPost(int size)
        {
            return DbContext.Set<Post>().OrderByDescending(p => p.ViewCount).Take(size).ToList();
        }

        public IList<Post> GetPostsByCategory(string category)
        {
            return DbContext.Set<Post>().Where(p => p.Category.Name.Contains(category)).ToList();
        }

        public IList<Post> GetPostsByMonth(DateTime monthYear)
        {
            return DbContext.Set<Post>().Where(p => p.PostedOn.Year == monthYear.Year && p.PostedOn.Month == monthYear.Month).ToList();
        }

        public IList<Post> GetPostsByTag(string tag)
        {
            return DbContext.Set<Post>().Where(p => p.Tags.Any(t => t.Name.Contains(tag))).ToList();
        }

        public IQueryable<Post> GetPublisedPosts()
        {
            return DbContext.Set<Post>().Where(p => p.Published);
        }

        public IQueryable<Post> GetPublishedToFilter(Expression<Func<Post, bool>> filter = null, Expression<Func<Post, bool>> filterByUser = null, Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Post> query = GetPublisedPosts();
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

        public IQueryable<Post> GetUnpublisedPosts()
        {
            return DbContext.Set<Post>().Where(p => !p.Published);
        }

        public IQueryable<Post> GetUnPublishedToFilter(Expression<Func<Post, bool>> filter = null, Expression<Func<Post, bool>> filterByUser = null, Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Post> query = GetUnpublisedPosts();
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

        public void UpdatePublish(long id, bool status)
        {
            Post postToUpdate = Find(id);
            postToUpdate.Published = status;
            if (status)
            {
                postToUpdate.PostedOn = DateTime.Now;
            }
            Update(postToUpdate);
        }
    }
}
