using FA.JustBlog.Core.Models;
using System.Collections.Generic;

namespace FA.JustBlog.Core.Repositories
{
    public interface ITagRepository : IGerericRepository<Tag, int>
    {
        Tag GetTagByUrlSlug(string urlSlug);
        IList<Tag> GetPopularTags(int size);
    }
}
