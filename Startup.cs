using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolMangement.Startup))]
namespace SchoolMangement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
