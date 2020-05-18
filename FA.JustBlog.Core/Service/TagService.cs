using System;
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Core.Infrastructure;

namespace FA.JustBlog.Core.Service
{
    public class TagService : GenericService<Tag, int>, ITagService
    {
        ITagRepository repository;
        public TagService(ITagRepository _repository, IUnitOfWork _unitOfWork) : base(_repository, _unitOfWork)
        {
            repository = _repository;
        }

        public ResponseModel GetPopularTags(int size)
        {
            if (size <= 0)
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetPopularTags(size);
                    return Common.ResponseData.Response(Status: 200, Message: "Success", Data: getResult);
                }
                catch (Exception ex)
                {
                    return Common.ResponseData.Response(Status: 302, Message: "Error: " + ex.Message);
                }
            }
        }

        public ResponseModel GetTagByUrlSlug(string urlSlug)
        {
            if (string.IsNullOrEmpty(urlSlug))
            {
                return Common.ResponseData.Response(Status: 302, Message: "Input Invalid");
            }
            else
            {
                try
                {
                    var getResult = repository.GetTagByUrlSlug(urlSlug);
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
