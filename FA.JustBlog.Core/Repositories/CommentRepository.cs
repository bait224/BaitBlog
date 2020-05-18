using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Core.Repositories
{
    public class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
    {
        public CommentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        /// <summary>
        /// Implement Add method to add a comment
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="commentName"></param>
        /// <param name="commentEmail"></param>
        /// <param name="commentTitle"></param>
        /// <param name="commentBody"></param>
        public void Add(long postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            Comment newComment = new Comment()
            {
                Name = commentName,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody,
                PostId = postId
            };

            DbContext.Set<Comment>().Add(newComment);
        }
        /// <summary>
        /// Implement GetCommentsForPost to get comments of post by postID
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public IList<Comment> GetCommentsForPost(long postId)
        {
            return DbContext.Set<Comment>().Where(c => c.PostId == postId).ToList(); 
        }
        /// <summary>
        /// Implement GetCommentsForPost to get comments of post by Post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public IList<Comment> GetCommentsForPost(Post post)
        {
            return DbContext.Set<Comment>().Where(c => c.Post == post).ToList();
        }
    }
}
