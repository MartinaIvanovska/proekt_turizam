using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(proekt_turizam.Startup))]
namespace proekt_turizam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminUserAndRole();
        }
    }
}
