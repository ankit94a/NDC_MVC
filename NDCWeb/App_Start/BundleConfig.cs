using System.Web;
using System.Web.Optimization;

namespace NDCWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/mainjs").Include("~/Component/js/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Component/js/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/jquistyle").Include("~/Component/js/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Component/js/jquery-ui/jquery-ui.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Component/js/modernizr-*"));

                bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                          "~/Component/js/bootstrap.js"));


           
            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Component/css/bootstrap.min.css",
                          "~/Component/css/style.css",
                          "~/Component/css/index.css",
                          "~/Component/css/main.css",
                          "~/Component/custom/css/intlTelInput.css"));

            bundles.Add(new StyleBundle("~/Content/cmssec").Include("~/Component/css/bootstrap.min.css", new CssRewriteUrlTransform()));
            bundles.Add(new StyleBundle("~/Component/cmsup").Include("~/Component/cmsupify/uploadify.css", new CssRewriteUrlTransform()));

                bundles.Add(new ScriptBundle("~/bundles/cmsup").Include("~/Component/cmsupify/jquery.uploadify.min.js"));




                ///////////////////------Admin Panel---------------///////////
                bundles.Add(new StyleBundle("~/Admin/css").Include(
                      "~/Areas/Admin/Component/css/main.css"));
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                     "~/Areas/Admin/Component/scripts/main.js"));


            //----Styles----//
            // add comment by yogendra
            bundles.Add(new StyleBundle("~/bundles/datatables/css").Include(
                      "~/Component/DataTable/jQueryUI-1.12.1/themes/base/jquery-ui.min.css",
                      "~/Component/DataTable/datatables.min.css"));
            // end comment by yogendra

            // add by yogendra
            bundles.Add(new StyleBundle("~/bundles/jquistyle").Include("~/Component/js/jquery-ui/jquery-ui.css", new CssRewriteUrlTransform()));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Component/js/jquery-ui/jquery-ui.js"));
            bundles.Add(new StyleBundle("~/bundles/datatable/css").Include("~/Component/DataTble/datatables.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include("~/Component/DataTble/datatables.min.js"));

            //end by yogendra

            bundles.Add(new StyleBundle("~/bundles/datatables/ButtonCss").Include(
                      "~/Component/DataTable/Buttons-2.2.2/css/buttons.dataTables.min.css",
                      "~/Component/DataTable/Buttons-2.2.2/css/buttons.jqueryui.min.css"));

            bundles.Add(new StyleBundle("~/bundles/css/jstreecss").Include(
                      "~/Component/treeview/css/jstree/themes/default/style.css"));

            //----Scripts----//
            // add comment by yogendra

            //bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
            //         "~/Component/DataTable/datatables.min.js"));

            // end comment by yogendra

            //bundles.Add(new ScriptBundle("~/bundles/datatables/ButtonJs").Include(
            //        "~/Component/DataTable2/Buttons-1.6.1/js/dataTables.buttons.min.js",
            //         "~/Component/DataTable2/JSZip-2.5.0/jszip.min.js",
            //         "~/Component/DataTable2/pdfmake-0.1.36/pdfmake.min.js",
            //         "~/Component/DataTable2/pdfmake-0.1.36/vfs_fonts.js",
            //         "~/Component/DataTable2/Buttons-1.6.1/js/buttons.colVis.min.js",
            //         "~/Component/DataTable2/Buttons-1.6.1/js/buttons.html5.min.js",
            //         "~/Component/DataTable2/Buttons-1.6.1/js/buttons.print.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jstree").Include(
                     "~/Component/treeview/js/jstree.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                     "~/Component/custom/js/customScript.js",
                     "~/Component/custom/js/intlTelInput.js"));

            bundles.Add(new ScriptBundle("~/bundles/0").Include(
                     "~/Component/custom/js/courseMemberScript.js"));

            #region Notification
            bundles.Add(new StyleBundle("~/bundles/toastr/css").Include(
                      "~/Component/Notification/toastr/toastr2.1.3.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr/js").Include(
                     "~/Component/Notification/toastr/toastr2.1.3.min.js",
                     "~/Component/Notification/toastr/toastrCustom.js"));
            #endregion

            bundles.Add(new ScriptBundle("~/bundles/daterange").Include(
                     "~/Content/plugins/pickers/daterange/moment.min.js",
                     "~/Content/plugins/pickers/daterange/daterangepicker.js"));
        }
    }
}
