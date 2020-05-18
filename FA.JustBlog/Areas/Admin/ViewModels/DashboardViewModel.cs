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
}