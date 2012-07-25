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

        [Test]
        public void CanFindAuthor()
        {
            var user = new User
            {
                Email = "payam@yavari.se",
                Username = "pyavari",
                Documents = new List<Document>
                {
                    new Document
                        {
                            Text = "This is a document!"
                        }
                }
            };
            user.Save();

            var document = Document.Find(user.Documents.First().Id);
            Assert.AreEqual("pyavari", document.Author.Username);
        }

        [Test]
        public void UserShouldBeAbleToTagDocumentsAsFavourites()
        {
            var user = new User
            {
                Documents = new List<Document> {new Document {Text = "This is an interesting docuemnt."}},
                Favourites = new List<Favourite>()
            };
            user.Save();
            var documentId = user.Documents.First().Id;
            user.TagFavourite(documentId);

            Assert.AreEqual(documentId, user.Favourites.First().Document.Id);

        }
    }
}
