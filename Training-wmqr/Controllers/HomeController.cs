using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.ActiveRecord;
using Training_wmqr.Infrastructure;
using Training_wmqr.Models;

namespace Training_wmqr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (new SessionScope())
            {
                ViewBag.Documents = Serializer.ToJSON(Document.FindAll().Select(d => new {d.Id, d.Text} ));
                return View("Index");
            }
        }
    }
}
