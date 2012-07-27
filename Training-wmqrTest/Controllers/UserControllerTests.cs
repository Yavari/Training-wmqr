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
    public class UserControllerTests : ModelTests
    {
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new UserController { _user = MockCurrentUser() };
            var user = new User
            {
                Email = "email@tset.com",
                Username = "username"
            };
            user.Save();
        }

        [Test]
        public void CanIndex()
        {
            dynamic result = _controller.Index();
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOf<IEnumerable<User>>(result.Model);
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
            var user = new User
            {
                Username = "pyavari"
            };
            dynamic result = _controller.Create(user);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual(2, User.FindAll().Count());
            Assert.AreEqual("pyavari", User.Find(2).Username);
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
            var user = User.Find(1);
            user.Email = "new@email.com";
            dynamic result = _controller.Edit(1, user);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("new@email.com", User.Find(1).Email);
        }

        [Test]
        public void CanDelete()
        {
            dynamic result = _controller.Delete(1);
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual(0, User.FindAll().Count());
        }
    }
}
