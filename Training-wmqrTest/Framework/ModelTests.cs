using System.Reflection;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using Moq;
using Training_wmqr.Infrastructure;
using log4net.Config;
using NUnit.Framework;

namespace Training_wmqrTest.Framework
{
    public class ModelTests
    {
        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            if (ActiveRecordStarter.IsInitialized) return;
            
            IConfigurationSource source = ActiveRecordSectionHandler.Instance;
            XmlConfigurator.Configure();
            ActiveRecordStarter.Initialize(Assembly.Load("Training-wmqr"), source);

        }

        [SetUp]
        public void SetUp()
        {
            ActiveRecordStarter.CreateSchema();
            ScopeManagement.CreateScope();
        }

        [TearDown]
        public void TearDownFixture()
        {
            ScopeManagement.DisposeScope();
            ActiveRecordStarter.DropSchema();
        }

        protected virtual void ResetScope()
        {
            ScopeManagement.ResetScope();
        }

        public ICurrentUser MockCurrentUser(string username = "lvaezi")
        {
            var userMock = new Mock<ICurrentUser>();
            userMock.Setup(x => x.Name()).Returns(username);
            userMock.Setup(x => x.UserName()).Returns(username);
            return userMock.Object;
        }
    }
}