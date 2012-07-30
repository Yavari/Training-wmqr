using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
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
            ResetScope();

            var document = Document.Find(user.Documents.First().Id);
            Assert.AreEqual("pyavari", document.Author.Username);
        }

        [Test]
        public void IShouldBeAbleToTagFavrouriteDocuments()
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

        [Test]
        public void CanFindDocumentsByUser()
        {
            var user = new User
            {
                Documents = new List<Document> {new Document {Text = "This is an interesting docuemnt."}},
                Favourites = new List<Favourite>()
            };
            user.Save();
            ResetScope();

            user = User.Find(user.Id);
            Assert.AreEqual(1, user.Documents.Count);
        }

        [Test]
        public void CanFindUserByUsername()
        {
            var user = new User {Username = "pyavari"};
            user.Save();

            Assert.IsNotNull(User.FindByUsername("pyavari"));
        }

        [Test]
        public void FindByUsernameThrowsExceptionIfNotFound()
        {
            var exception = Assert.Throws<NotFoundException>(()=> User.FindByUsername("pyavari"));
            Assert.AreEqual("'pyavari' not found", exception.Message);
        }

        [Test]
        public void CanAddFavourite()
        {
            var user = new User
            {
                Documents = new List<Document> {new Document {Text = "Favourite"}},
                Favourites = new List<Favourite>()
            };
            user.Save();
            using (new SessionScope())
            {
                User.Find(user.Id).AddFavourite(1);
            }

            using (new SessionScope(FlushAction.Never))
            {
                Assert.AreEqual(1, User.Find(user.Id).Favourites.Count);
                Assert.AreEqual(1, Favourite.FindAll().Count());
            }
        }

        [Test]
        public void WontAddSameDocumentToFavourite()
        {
            var user = new User
            {
                Documents = new List<Document> { new Document { Text = "Favourite" } },
                Favourites = new List<Favourite>()
            };
            user.Favourites.Add(new Favourite{Document = user.Documents.First()});
            user.Save();

            using (new SessionScope())
            {
                User.Find(user.Id).AddFavourite(1);
            }
            
            using (new SessionScope(FlushAction.Never))
            {
                Assert.AreEqual(1, User.Find(user.Id).Favourites.Count);
            }
        }

        [Test]
        public void CanRemoveFavourite()
        {
            var user = new User
            {
                Documents = new List<Document> { new Document { Text = "Favourite" } },
                Favourites = new List<Favourite>()
            };
            user.Favourites.Add(new Favourite { Document = user.Documents.First() });
            user.Save();
            ResetScope();

            User.Find(user.Id).RemoveFavourite(1);
            ResetScope();

            Assert.AreEqual(0, User.Find(user.Id).Favourites.Count);
            Assert.AreEqual(0, Favourite.FindAll().Count());
        }
    }
}
