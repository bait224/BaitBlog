using System;

namespace FA.JustBlog.Core.Common
{
    public class ResponseModel
    {
        public int Status { get; set; }
        public string MessageText { get; set; }
        public Object Data { get; set; }
    }
}
