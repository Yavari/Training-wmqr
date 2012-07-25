using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using Training_wmqr.Controllers;
using Training_wmqr.Models;
using Training_wmqrTest.Framework;

namespace Training_wmqrTest.Controllers
{
    [TestFixture]
    public class DocumentControllerTests : ModelTests
    {
        private DocumentController _controller;

        [SetUp]
        public void Setup()
        {
            var author = new User
            {
                Username = "username",
                Email = "mail@gmail.com",
                Documents = new List<Document>
                {
                    new Document{Text = "This is a test document"}
                }
            };
            author.Save();
            _controller = new DocumentController();
        }

        [Test]
        public void CanIndex()
        {
            var result = _controller.Index() as ViewResult;
            var model = result.Model as IEnumerable<Document>;
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(1, model.Count());
        }

        [Test]
        public void CanDetails()
        {
            dynamic result = _controller.Details(1);
            Assert.AreEqual("Details", result.ViewName);
            Assert.AreEqual(1, result.Model.Id);
        }

        [Test]
        public void CanCreate()
        {
            dynamic result = _controller.Create();
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void CanPostCreate()
        {
            var document = new Document
            {
                Text = "New document"
            };
            var result = _controller.Create(document) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual(2, Document.FindAll().Count());
        }

        [Test]
        public void CanEdit()
        {
            dynamic result = _controller.Edit(1);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual(1, result.Model.Id);
        }

        [Test]
        public void CanPostEdit()
        {
            var document = Document.Find(1);
            document.Text = "New";
            dynamic result = _controller.Edit(1, document);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("New", Document.Find(1).Text);
        }

        [Test]
        public void CanDelete()
        {
            dynamic result = _controller.Delete(1);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual(0, Document.FindAll().Count());
        }
    }
}
