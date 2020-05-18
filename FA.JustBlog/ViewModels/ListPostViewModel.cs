using FA.JustBlog.Common;
using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FA.JustBlog.ViewModels
{
    public class ListPostViewModel
    {
        public PaginationList<Post> ListPost { get; set; }
        public ListPostTypeToIndex Type { get; set; }
        public string ByWhat { get; set; }
    }
}