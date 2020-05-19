using System.Collections.Generic;

namespace FA.JustBlog.Areas.Admin.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalPost { get; set; }
        public int PublishedCount { get; set; }
        public int UnPublishedCount { get; set; }
        public DashboardViewModel()
        {
            TotalPost = 0;
            PublishedCount = 0;
            UnPublishedCount = 0;
        }
    }

    public class CategoryChart
    {
        public string Category { get; set; }
        public int CountPost { get; set; }
    }
}