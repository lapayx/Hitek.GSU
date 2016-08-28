using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(Hitek.GSU.Startup))]
namespace Hitek.GSU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            DIConfig.Register(config);
            ConfigureAuth(app);
            //HttpConfiguration config = new HttpConfiguration();
            // WebApiConfig.Register(config);
            //  app.UseWebApi(config);

            AreaRegistration.RegisterAllAreas();
           // GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}
