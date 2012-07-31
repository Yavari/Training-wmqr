using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using SolrNet;
using Training_wmqr.Models;

namespace Training_wmqr
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var solrUrl = @"http://localhost:8983/solr";
            Startup.Init<Dictionary<string, object>>(solrUrl);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            InitializeActiveRecord();
        }

        private void InitializeActiveRecord()
        {
            if (ActiveRecordStarter.IsInitialized) return;
            var source = ActiveRecordSectionHandler.Instance;
            ActiveRecordStarter.Initialize(Assembly.Load("Training-wmqr"), source);
            ActiveRecordStarter.CreateSchema();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_EndRequest()
        {
            if (HttpContext.Current.Items.Contains("ar.session"))
            {
                var session = HttpContext.Current.Items["ar.session"] as SessionScope;
                session.Dispose();
            }
        }
    }

}