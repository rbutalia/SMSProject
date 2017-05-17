
using System;
using System.Reflection;
using Notifications.Entities;
using Repository.Pattern.Ef6;
using Notifications.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        [TestMethod]
        public void UnitOfWork_Transaction_Test()
        {
            using(IDataContextAsync context = new NotificationsContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
               // IService<Customer> customerService = new CustomerService(customerRepository);

                try
                {
                    unitOfWork.BeginTransaction();
                
                    //customerService.Insert(new Customer { CustomerID = "YODA", CompanyName = "SkyRanch", ObjectState = ObjectState.Added});
                    //customerService.Insert(new Customer { CustomerID = "JEDI", CompanyName = "SkyRanch", ObjectState = ObjectState.Added});

                    //var customer = customerService.Find("YODA");
                    //Assert.AreSame(customer.CustomerID, "YODA");

                    //customer = customerService.Find("JEDI");
                    //Assert.AreSame(customer.CustomerID, "JEDI");

                    //// save
                    //var saveChangesAsync = unitOfWork.SaveChanges();
                    ////Assert.AreSame(saveChangesAsync, 2);

                    //// Will cause an exception, cannot insert customer with the same CustomerId (primary key constraint)
                    //customerService.Insert(new Customer { CustomerID = "JEDI", CompanyName = "SkyRanch", ObjectState = ObjectState.Added });
                    ////save 
                    unitOfWork.SaveChanges();

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                }
            }
        }

        [TestMethod]
        public void UnitOfWork_Dispose_Test()
        {
            IDataContextAsync context = new NotificationsContext();
            IUnitOfWorkAsync unitOfWork = new UnitOfWork(context);

            // opening connection
            unitOfWork.BeginTransaction();
            unitOfWork.Commit();

            // calling dispose 1st time
            unitOfWork.Dispose();
            var isDisposed = (bool) GetInstanceField(typeof (UnitOfWork), unitOfWork, "_disposed");
            Assert.IsTrue(isDisposed);

            // calling dispose 2nd time, should not throw any excpetions
            unitOfWork.Dispose();
            context.Dispose();

            // calling dispose 3rd time, should not throw any excpetions
            context.Dispose();
            unitOfWork.Dispose();
        }

        [TestMethod]
        public void IDataContext_Dispose_Test()
        {
            IDataContextAsync context = new NotificationsContext();
            IUnitOfWorkAsync unitOfWork = new UnitOfWork(context);

            // opening connection
            unitOfWork.BeginTransaction();
            unitOfWork.Commit();

            // calling dispose 1st time
            context.Dispose();

            var isDisposed = (bool) GetInstanceField(typeof (DataContext), context, "_disposed");
            Assert.IsTrue(isDisposed);

            // calling dispose 2nd time, should not throw any excpetions
            unitOfWork.Dispose();
            context.Dispose();

            // calling dispose 3rd time, should not throw any excpetions
            unitOfWork.Dispose();
            context.Dispose();
        }

        private static object GetInstanceField(Type type, object instance, string fieldName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var field = type.GetField(fieldName, bindFlags);
            return field != null ? field.GetValue(instance) : null;
        }

        public TestContext TestContext { get; set; }
    }
}