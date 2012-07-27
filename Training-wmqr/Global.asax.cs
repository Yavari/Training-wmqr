using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using Training_wmqr.Models;

namespace Training_wmqr
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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
            //ActiveRecordStarter.CreateSchema();
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}