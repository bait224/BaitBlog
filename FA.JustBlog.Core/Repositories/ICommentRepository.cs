using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;

namespace FA.JustBlog.Core.Repositories
{
    public interface ICommentRepository : IGerericRepository<Comment, Guid>
    {
        void Add(long postId, string commentName, string commentEmail, string commentTitle,string commentBody);
        IList<Comment> GetCommentsForPost(long postId);
        IList<Comment> GetCommentsForPost(Post post);
    }
}
