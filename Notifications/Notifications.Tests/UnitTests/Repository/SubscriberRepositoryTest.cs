
using System;
using Repository.Pattern.Ef6;
using Notifications.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Notifications.Tests.UnitTests.Fake;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Notifications.Tests.UnitTests.Repository
{
    [TestClass]
    public class SubscriberRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // todo: delete NorthwindTest.mfd (LocalDb)
            // cleanup all the infrastructure that was needed for our tests.
        }

        [TestMethod]
        public void DeleteSubscriberById()
        {
            using (IDataContextAsync notificationsFakeContext = new NotificationsFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(notificationsFakeContext))
            {
                unitOfWork.Repository<Subscriber>().Insert(new Subscriber { SubscriberID = 1, CustomerId = 1, PhoneNumber = "+17165414925", IsActive = true, Subscribed=true, CreatedDate=DateTime.Now, ModifiedDate=DateTime.Now, ObjectState = ObjectState.Added });
                unitOfWork.SaveChanges();
                unitOfWork.Repository<Subscriber>().Delete(1);
                unitOfWork.SaveChanges();
                var sub = unitOfWork.Repository<Subscriber>().Find(1);
                Assert.IsNull(sub);
            }
        }
    }
}
