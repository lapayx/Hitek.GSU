using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(Hitek.GSU.Startup))]
namespace Hitek.GSU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DIConfig.Register();
            ConfigureAuth(app);
            //HttpConfiguration config = new HttpConfiguration();
           // WebApiConfig.Register(config);
          //  app.UseWebApi(config);
        }
    }
}
