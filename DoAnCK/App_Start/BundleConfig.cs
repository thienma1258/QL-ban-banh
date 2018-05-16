using System.Web;
using System.Web.Optimization;

namespace project
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.4.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/front-js").Include(
                "~/Scripts/jquery-2.1.4.min.js",
                "~/Scripts/jquery.wmuSlider.js",
                "~/Scripts/megamenu.js",
                "~/Scripts/move-top.js",
                "~/Scripts/easing.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/ modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/megamenu.css",
                      "~/Content/style.css",
                    "~/Content/bootstrap.css"
                     ));

            bundles.Add(new StyleBundle("~/Admin/css").Include(
                  "~/Content/bootstrap.css",
                  "~/Content/style-admin.css",
                  "~/Content/font-awesome.min.css"
                 ));
            bundles.Add(new ScriptBundle("~/bundles/admin-js").Include(
             "~/Scripts/jquery-2.1.4.min.js",
           //"~/Scripts/jquery.nicescroll.js",
           //"~/Scripts/scripts.js",
           "~/Scripts/bootstrap.js"
                 ));
        }
    }
}
