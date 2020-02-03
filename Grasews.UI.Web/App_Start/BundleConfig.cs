using System.Web.Optimization;

namespace Grasews.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region js

            #region jquery js

            bundles.Add(new ScriptBundle("~/bundles/scripts/jquery").Include(
                "~/Content/plugins/jQuery/jquery-{version}.js",
                "~/Content/plugins/jQuery/jquery.validate.js",
                "~/Content/plugins/jQuery/jquery.validate.unobtrusive.js",
                "~/Content/plugins/jQueryUI/jquery-ui.js"));

            #endregion jquery js

            #region bootstrap js

            bundles.Add(new ScriptBundle("~/bundles/scripts/bootstrap").Include("~/Content/plugins/bootstrap/js/bootstrap.js"));

            #endregion bootstrap js

            #region codemirror js

            bundles.Add(new ScriptBundle("~/bundles/scripts/codemirror").Include(
                "~/Content/plugins/code-mirror/codemirror.js",
                "~/Content/plugins/code-mirror/mark-selection.js",
                "~/Content/plugins/code-mirror/language/xml.js"));

            #endregion codemirror js

            #region cytoscape js

            bundles.Add(new ScriptBundle("~/bundles/scripts/cytoscape").Include(
                "~/Content/plugins/cytoscape/cytoscape.js",
                "~/Content/plugins/cytoscape/dagre.js",
                "~/Content/plugins/cytoscape/cytoscape-dagre.js",
                "~/Content/plugins/cytoscape/cytoscape-cxtmenu.js"));

            #endregion cytoscape js

            #region select2 js

            bundles.Add(new ScriptBundle("~/bundles/scripts/select2").Include("~/Content/plugins/select2/select2.full.js"));

            #endregion select2 js

            #region signalr js

            bundles.Add(new ScriptBundle("~/bundles/scripts/signalr").Include("~/Content/plugins/signalr/jquery.signalR-{version}.js"));

            #endregion signalr js

            #region toastr js

            bundles.Add(new ScriptBundle("~/bundles/scripts/toastr").Include("~/Content/plugins/toastr/toastr.js"));

            #endregion toastr js

            #region iCheck js

            bundles.Add(new ScriptBundle("~/bundles/scripts/icheck").Include("~/Content/plugins/iCheck/icheck.js"));

            #endregion iCheck js

            #region site js

            bundles.Add(new ScriptBundle("~/bundles/scripts/site").Include(
                "~/Content/js/codemirror/grasews-codemirror.js",
                "~/Content/js/context-menus/grasews-context-menu.js",
                "~/Content/js/cytoscape/grasews-cytoscape.js",
                "~/Content/js/signalR/serviceDescriptionSignalR.js",
                "~/Content/js/AdminLTE.js",
                "~/Content/js/site.js",
                "~/Content/js/skins-manager.js"));

            #endregion site js

            #region login js

            bundles.Add(new StyleBundle("~/bundles/scripts/login").Include("~/Content/js/login.js"));

            #endregion login js

            #region admin js

            bundles.Add(new StyleBundle("~/bundles/scripts/admin").Include("~/Content/js/admin.js"));

            #endregion admin js

            #endregion js

            #region css

            #region site css

            bundles.Add(new StyleBundle("~/bundles/styles/site").Include(
                "~/Content/css/AdminLTE.css",
                "~/Content/css/site.css",
                "~/Content/css/toastr.css",
                "~/Content/css/skins/_all-skins.css"));

            #endregion site css

            #region login css

            bundles.Add(new StyleBundle("~/bundles/styles/login").Include("~/Content/css/login.css"));

            #endregion login css

            #region admin css

            bundles.Add(new StyleBundle("~/bundles/styles/admin").Include("~/Content/css/admin.css"));

            #endregion admin css

            #region bootstrap css

            bundles.Add(new StyleBundle("~/bundles/styles/bootstrap").Include(
                "~/Content/plugins/bootstrap/css/bootstrap-theme.css",
                "~/Content/plugins/bootstrap/css/bootstrap.css"));

            #endregion bootstrap css

            #region codemirror css

            bundles.Add(new StyleBundle("~/bundles/styles/codemirror").Include(
                "~/Content/plugins/code-mirror/themes/eclipse.css",
                "~/Content/plugins/code-mirror/codemirror.css"));

            #endregion codemirror css

            #region select2 css

            bundles.Add(new StyleBundle("~/bundles/styles/select2").Include("~/Content/plugins/select2/select2.css"));

            #endregion select2 css

            #region toastr css

            bundles.Add(new StyleBundle("~/bundles/styles/toastr").Include("~/Content/plugins/toastr/toastr.css"));

            #endregion toastr css

            #region iCheck css

            bundles.Add(new StyleBundle("~/bundles/styles/icheck").Include("~/Content/plugins/iCheck/square/purple.css"));

            #endregion iCheck css

            #region fontawesome css

            //bundles.Add(new StyleBundle("~/bundles/styles/fontawesome").Include(
            //    "~/Content/plugins/font-awesome/font-awesome.css",
            //    "~/Content/plugins/font-awesome/font-awesome-animation.css"));

            #endregion fontawesome css

            #endregion css

            //BundleTable.EnableOptimizations = false;
        }
    }
}