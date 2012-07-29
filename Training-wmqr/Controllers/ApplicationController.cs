using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_wmqr.Infrastructure;

namespace Training_wmqr.Controllers
{
    public class ApplicationController : Controller
    {
        public Castle.ActiveRecord.SessionScope Scope { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!HttpContext.Items.Contains("ar.session"))
            {
                Scope = new Castle.ActiveRecord.SessionScope();
                HttpContext.Items.Add("ar.session", Scope);
                base.OnActionExecuting(filterContext);
            }
        }
        public ICurrentUser _user = new CurrentUser();

        public ApplicationController()
        {
            _user = new CurrentUser();
        }
    }
}
