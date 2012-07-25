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
    public class DocumentTests : ModelTests
    {
        [Test]
        public void CanSaveDocument()
        {
            var document = new Document
            {
                Text = "This is a document."
            };
            document.Save();

            document = Document.Find(document.Id);
            Assert.AreEqual("This is a document.", document.Text);
        }
    }
}
