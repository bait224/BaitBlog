using System.Web.Optimization;

namespace FA.JustBlog
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Assets/js/bootstrap.min.js",
                      "~/Assets/js/respond.min.js",
                      "~/Assets/js/bootstrap.bundle.min.js",
                      "~/Assets/js/clean-blog.min.js",
                      "~/Assets/js/jquery-ui.min.js",
                      "~/Assets/js/jquery.min.js",
                      "~/Assets/js/raphael.min.js",
                      "~/Assets/js/morris.min.js",
                      "~/Assets/js/jquery.sparkline.min.js",
                      "~/Assets/js/bootstrap3-wysihtml5.all.min.js",
                      "~/Assets/js/jquery.slimscroll.min.js",
                      "~/Assets/js/fastclick.js",
                      "~/Assets/js/adminlte.min.js",
                      "~/Assets/js/dashboard.js",
                      "~/Assets/js/demo.js",
                      "~/Assets/js/jquery-jvectormap-1.2.2.min.js",
                      "~/Assets/js/jquery-jvectormap-world-mill-en.js",
                      "~/Assets/js/moment.min.js",
                      "~/Assets/admin_assets/js/slug.js",
                      "~/Assets/js/bootstrap-datetimepicker.min.js",
                      "~/Assets/canvasjs-2.3.2/canvasjs.min.js",
                      "~/Assets/canvasjs-2.3.2/jquery.canvasjs.min.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap.min.css",
                      "~/Assets/css/site.css",
                      "~/Assets/css/clean-blog.min.css",
                      "~/Assets/Ionicons/css/ionicons.min.css",
                      "~/Assets/css/AdminLTE.min.css",
                      "~/Assets/css/_all-skins.min.css",
                      "~/Assets/css/jquery-jvectormap.css",
                      "~/Assets/css/daterangepicker.css",
                      "~/Assets/css/morris.css",
                      "~/Assets/css/bootstrap3-wysihtml5.min.css",
                      "~/Assets/css/bootstrap-datetimepicker.min.css"
                      ));

            bundles.Add(new StyleBundle("~/bundles/fontawesome-free").Include(
                      "~/Assets/font-awesome/css/font-awesome.min.css"));



        }
    }
}
