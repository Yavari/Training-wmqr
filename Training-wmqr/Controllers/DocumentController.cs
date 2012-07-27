using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.ActiveRecord;
using Training_wmqr.Models;

namespace Training_wmqr.Controllers
{
    public class DocumentController : ApplicationController
    {
        public ActionResult Index()
        {
            return View("Index", Document.FindAll());
        }

        public ActionResult Details(int id)
        {
            using (new SessionScope())
            {
                var document = Document.Find(id);
                document.Favourites.ToList();
                return View("Details", document);
            }
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Document document)
        {
            using(var transaction = new TransactionScope(TransactionMode.New))
            {
                try
                {
                    var user = _user.UserName();
                    var author = Models.User.FindByUsername(user);
                    author.Documents.Add(document);
                    author.Save();
                    return RedirectToAction("Index");
                }
                catch (NotFoundException ex)
                {
                    transaction.VoteRollBack();
                    ViewBag.ErrorMessage = String.Format("{0}. Please add the user first.", ex.Message);
                    
                }
            }
            return View("Create");
        }

        public ActionResult Edit(int id)
        {
            return View("Edit", Document.Find(id));
        }


        [HttpPost]
        public ActionResult Edit(int id, Document document)
        {
            var original =Document.Find(id);
            original.Text = document.Text;
            original.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var document = Document.Find(id);
            document.Delete();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddFavourite(int id)
        {
            using (new SessionScope())
            {
                var user = Models.User.FindByUsername(_user.UserName());
                user.AddFavourite(id);
                return RedirectToAction("Details", new{id});
            }
        }

        [HttpPost]
        public ActionResult RemoveFavourite(int id)
        {
            using (new SessionScope())
            {
                var user = Models.User.FindByUsername(_user.UserName());
                user.RemoveFavourite(id);
                return RedirectToAction("Details", new { id });
            }

            
        }
    }
}
