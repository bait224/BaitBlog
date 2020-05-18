using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(FA.JustBlog.Startup))]
namespace FA.JustBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
