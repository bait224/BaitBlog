using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using System;

namespace FA.JustBlog.Core.Service
{
    public interface ICommentService : IGenericService<Comment, Guid>
    {
        ResponseModel Add(long postId, string commentName, string commentEmail, string commentTitle, string commentBody);
        ResponseModel GetCommentsForPost(long postId);
        ResponseModel GetCommentsForPost(Post post);
    }
}
