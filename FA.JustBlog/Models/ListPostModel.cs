using FA.JustBlog.Core.Common;
using FA.JustBlog.Core.Models;
using System.Collections.Generic;

namespace FA.JustBlog.Models
{
    public class ListPostModel
    {
        public List<Post> ListPost { get; set; }
        public TypeListPostEnum Type { get; set; }
    }
}