using System.Security.Claims;

namespace FA.JustBlog.Common.UserHelpers
{
    public static class UserExtend
    {
        public static string GetFullName(this System.Security.Principal.IPrincipal user)
        {
            var fullNameClaim = ((ClaimsIdentity)user.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }
    }
}