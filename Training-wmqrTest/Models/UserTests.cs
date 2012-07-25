using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Training_wmqr.Models;
using Training_wmqrTest.Framework;

namespace Training_wmqrTest.Models
{
    [TestFixture]
    public class UserTests : ModelTests
    {
        [Test]
        public void CanSaveUser()
        {
            var user = new User
            {
                Email = "payam@yavari.se",
                Username = "pyavari"
            };
            user.Save();

            user = User.Find(user.Id);
            Assert.AreEqual("payam@yavari.se", user.Email);
            Assert.AreEqual("pyavari", user.Username);
        }
    }
}
