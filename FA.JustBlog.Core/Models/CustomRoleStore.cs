using Microsoft.AspNet.Identity.EntityFramework;

namespace FA.JustBlog.Core.Models
{
    public class CustomRoleStore : RoleStore<ApplicationRole, int, CustomUserRole>
    {
        public CustomRoleStore(JustBlogContext context) : base(context)
        {
        }
    }
}
