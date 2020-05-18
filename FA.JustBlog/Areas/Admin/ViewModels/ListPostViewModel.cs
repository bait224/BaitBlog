using FA.JustBlog.Areas.Common;
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class ListPostViewModel
    {
        public PaginationList<Post> ListPost { get; set; }
        public ListPostTypeForManage Type { get; set; }
        public int PageSize { get; set; }
    }
}