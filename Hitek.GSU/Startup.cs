using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hitek.GSU.Startup))]
namespace Hitek.GSU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DIConfig.Register();
            ConfigureAuth(app);
        }
    }
}
