using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using System;

namespace FA.JustBlog.Core.Service
{
    public class CommentService : GenericService<Comment, Guid>, ICommentService
    {
        private readonly ICommentRepository repository;
        public CommentService(ICommentRepository _repository, IUnitOfWork _unitOfWork) : base(_repository, _unitOfWork)
        {
            repository = _repository;
        }

        public ResponseModel Add(long postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            if (Guid.TryParse(postId.ToString(), out Guid checkId))
            {
                return Common.ResponseData.Response(Status: 302, Message: "PostId Invalid");
            }
            if (string.IsNullOrEmpty(commentName))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Name Invalid");
            }
            if (string.IsNullOrEmpty(commentEmail))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Email Invalid");
            }
            if (string.IsNullOrEmpty(commentTitle))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Comment Header Invalid");
            }
            if (string.IsNullOrEmpty(commentBody))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Comment Text Invalid");
            }

            try
            {
                repository.Add(postId,commentName, commentEmail, commentTitle, commentBody);
                _unitOfWork.Commit();
                return Common.ResponseData.Response(Status: 200, Message: "Add Success");
            }
            catch (Exception ex)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Add Fail - Error: " + ex.Message);
            }

        }

        public ResponseModel GetCommentsForPost(long postId)
        {
            if (Guid.TryParse(postId.ToString(), out Guid checkId))
            {
                return Common.ResponseData.Response(Status: 302, Message: "PostId Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetCommentsForPost(postId);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetCommentsForPost(Post post)
        {
            if (post == null)
            {
                return Common.ResponseData.Response(Status: 302, Message: "PostId Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetCommentsForPost(post);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }
    }
}
