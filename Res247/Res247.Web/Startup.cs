using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(Res247.Web.Startup))]
namespace Res247.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
