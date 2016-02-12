using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GMLWeb.Startup))]
namespace GMLWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
