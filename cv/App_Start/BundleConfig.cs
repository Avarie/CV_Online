using System.Web.Optimization;

namespace cv
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/textAngular.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
            "~/Scripts/angular.js",
            "~/Scripts/textAngular/textAngular.js",
            "~/Scripts/textAngular/textAngular-sanitize.js",
            "~/Scripts/textAngular/textAngularSetup.js",
            "~/Scripts/textAngular/textAngular-rangy.min.js",
            "~/Scripts/textAngular/rangy-core.js",
            "~/Scripts/textAngular/rangy-selectionsaverestore.js"
            ));
        }
    }
}
