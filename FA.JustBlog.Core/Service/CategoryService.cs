using FA.JustBlog.Core.Infrastructure;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;

namespace FA.JustBlog.Core.Service
{
    public class CategoryService : GenericService<Category, int>, ICatgoryService
    {
        public CategoryService(IGerericRepository<Category, int> _repository, IUnitOfWork _unitOfWork) : base(_repository, _unitOfWork)
        {
        }
    }
}
