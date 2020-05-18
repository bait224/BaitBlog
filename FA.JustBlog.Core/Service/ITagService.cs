using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core.Service
{
    public interface ITagService : IGenericService<Tag, int>
    {
        ResponseModel GetTagByUrlSlug(string urlSlug);
        ResponseModel GetPopularTags(int size);
    }
}
