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
        public ICurrentUser _user = new CurrentUser();

        public ApplicationController()
        {
            _user = new CurrentUser();
        }
    }
}
