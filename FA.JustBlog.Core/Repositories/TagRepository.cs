using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Core.Repositories
{
    public class TagRepository : GenericRepository<Tag, int>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<Tag> GetPopularTags(int size)
        {
            var listTag = DbContext.Set<Tag>().ToList();
            foreach(var item in listTag)
            {
                item.CountPost();
            }
            return listTag.OrderByDescending(t => t.Count).Take(size).ToList();
        }

        public Tag GetTagByUrlSlug(string urlSlug)
        {
            return DbContext.Set<Tag>().FirstOrDefault(t => t.UrlSlug == urlSlug);
        }
    }
}
