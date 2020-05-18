using Microsoft.AspNet.Identity.EntityFramework;

namespace FA.JustBlog.Core.Models
{
    public class CustomUserStore : UserStore<ApplicationUser, ApplicationRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(JustBlogContext context) : base(context)
        {
        }
    }
}
