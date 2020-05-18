using System.Data.Entity;

namespace FA.JustBlog.Core.Infrastructure
{
    public interface IDbFactory
    {
        DbContext Init();
    }
}