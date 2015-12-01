using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GameStore.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-validate").Include("~/Scripts/jquery.unobtrusive*",
                                                                              "~/Scripts/jquery.validate*"));
            

            bundles.Add(new ScriptBundle("~/bundles/account").Include("~/Scripts/Account/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/comment").Include("~/Scripts/Comment/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/game").Include("~/Scripts/Game/*.js"));


            bundles.Add(new StyleBundle("~/content/css").Include("~/Content/Styles/Tags.css",
                                                                 "~/Content/Styles/Main.css",
                                                                 "~/Content/Styles/Content.css"));

            bundles.Add(new StyleBundle("~/jquery-ui/css").Include("~/Scripts/jquery-ui/*.css"));
        }
    }
}