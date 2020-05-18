using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using System.Linq;

namespace FA.JustBlog.Core.Repositories
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        /// <summary>
        /// Override Delete method to delete a category and set null categoryId in post
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int id)
        {
            using (var dbTransaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    var listPostByCategory = DbContext.Set<Post>().Where(p => p.CategoryId == id);
                    if (listPostByCategory.Any())
                    {
                        foreach (var post in listPostByCategory)
                        {
                            post.CategoryId = null;
                        }
                    }
                    base.Delete(id);

                    dbTransaction.Commit();
                }
                catch
                {
                    dbTransaction.Rollback();
                }
            }
        }
    }
}
