using System.Web;
using System.Web.Optimization;

namespace Scouter.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                 "~/Scripts/lib/bootstrap.js"
                ,"~/Scripts/lib/knockout-{version}.js"
                ,"~/Scripts/lib/underscore.js"
                ,"~/Scripts/lib/H5F.js"
                ,"~/Scripts/app/FRCEventsDataService.js"
                ,"~/Scripts/app/FRCMatchesDataService.js"
                ,"~/Scripts/app/validationUtility.js"
                ,"~/Scripts/app/scouterDataService.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/teams").Include(
                 "~/Scripts/lib/bootstrap.js"
                , "~/Scripts/lib/knockout-{version}.js"
                , "~/Scripts/lib/underscore.js"
                , "~/Scripts/lib/H5F.js"
                , "~/Scripts/app/teamsDataService.js"
                , "~/Scripts/app/validationUtility.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/offline").Include(
                "~/Scripts/app/offlineUtility.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/lib/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/lib/jquery.unobtrusive*",
                        "~/Scripts/lib/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/bootstrap.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}