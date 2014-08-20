using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(tempestas_mons.web.Startup))]
namespace tempestas_mons.web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
