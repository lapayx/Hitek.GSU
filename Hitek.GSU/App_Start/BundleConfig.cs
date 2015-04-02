using System.Web;
using System.Web.Optimization;

namespace Hitek.GSU
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/vendor/jquery-{version}.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/vendor/bootstrap.js",
                        "~/Scripts/vendor/respond.js",
                        "~/Scripts/vendor/underscore.js",
                        "~/Scripts/vendor/backbone.js",

                        "~/Scripts/vendor/backbone.wreqr.js",
                        "~/Scripts/vendor/backbone.babysitter.js",
                        "~/Scripts/vendor/backbone.marionette.js"
                        )
                        .IncludeDirectory("~/Scripts/override/", "*.js", true)
                    );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/bootstrap-theme.css",
                      "~/Content/css/site.css"));
            bundles.Add(new ScriptBundle("~/Scripts/app")
                .Include("~/Scripts/app/app.js")


                .IncludeDirectory("~/Scripts/app/Pages/", "*.js", true)/*
                .IncludeDirectory("~/Scripts/app/Item/", "*.model.js", true)
                .IncludeDirectory("~/Scripts/app/Item/", "*.collection.js", true)
                .IncludeDirectory("~/Scripts/app/Item/", "*.view.js", true)
                .IncludeDirectory("~/Scripts/app/Action/", "*.js", true)*/
                        //.IncludeDirectory("~/Scripts/app/modul/", "*.js", true)
                        // .IncludeDirectory("~/Scripts/app/model/", "*.js", true)
                        //  .IncludeDirectory("~/Scripts/app/collection/", "*.js", true)
                        // .IncludeDirectory("~/Scripts/app/view/", "*.js", true)
                );
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
