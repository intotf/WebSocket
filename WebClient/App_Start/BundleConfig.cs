using System.Web;
using System.Web.Optimization;

namespace WebClient
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Styles")
                         .Include("~/Content/bootstrap/dist/css/bootstrap.css")
                         .Include("~/Content/Site.css")
                         );

            bundles.Add(new ScriptBundle("~/Scripts")
                        .Include("~/Scripts/jqueryV2.2.3.js")
                        .Include("~/Content/layer/layer.js")
                        .Include("~/Content/bootstrap/dist/js/bootstrap.js")
                    );
        }
    }
}