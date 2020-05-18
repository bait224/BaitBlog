using System.Data.Entity;

namespace FA.JustBlog.Core.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DbContext _dbContext;

        public DbFactory(DbContext context)
        {
            _dbContext = context;
        }

        public DbContext Init()
        {
            return _dbContext;
        }
    }
}
