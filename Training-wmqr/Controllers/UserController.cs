using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training_wmqr.Models;

namespace Training_wmqr.Controllers
{
    public class UserController : ApplicationController
    {

        public ActionResult Index()
        {
            return View("Index", Models.User.FindAll());
        }


        public ActionResult Details(int id)
        {
            return View("Details", Models.User.Find(id));
        }


        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            user.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View("Edit", Models.User.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            var original = Models.User.Find(id);
            original.Email = user.Email;
            original.Username = user.Username;
            original.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = Models.User.Find(id);
            user.Delete();
            return RedirectToAction("Index");
        }
    }
}
