using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}