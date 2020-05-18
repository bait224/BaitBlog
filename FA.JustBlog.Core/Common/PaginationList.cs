using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Common
{
    public class PaginationList<TEntity> : List<TEntity>
    {
        public PaginationList(List<TEntity> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
            Counts = count;
        }

        public int PageIndex { get; }
        public int TotalPages { get; }
        public int Counts { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginationList<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageIndex,
            int pageSize)
        {
            var count = await source.CountAsync();
            var entities = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationList<TEntity>(entities, count, pageIndex, pageSize);
        }
    }
}
