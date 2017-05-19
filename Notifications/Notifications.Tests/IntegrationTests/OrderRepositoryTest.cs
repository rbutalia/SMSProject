
using System;
using System.Text;
using System.Diagnostics;
using Repository.Pattern.Ef6;
using Notifications.Entities;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Repository.Pattern.UnitOfWork;
using Notifications.Entities.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        public TestContext TestContext { get; set; }
        private const string SYS_USER = "TEST";
        [TestInitialize]
        public void Initialize()
        {
            //Utility.CreateSeededTestDatabase();
        }

        [TestMethod]
        public void CreateOrderObjectGraphTest()
        {
            using (IDataContextAsync context = new NotificationsContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Order> orderRepository = new Repository<Order>(context, unitOfWork);

                var orderTest = new Order
                {
                    CustomerID = 1,
                    //EmployeeID = 10,
                    OrderDate = DateTime.Now,
                    CreatedBy = SYS_USER,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = SYS_USER,
                    ModifiedDate = DateTime.Now,
                    ObjectState = ObjectState.Added,

                    //Employee = new Employee
                    //{
                    //    EmployeeID = 10,
                    //    FirstName = "Test",
                    //    LastName = "Le",
                    //    ObjectState = ObjectState.Added
                    //},

                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            MenuItemID = 1,
                            Quantity = 5,
                            CreatedBy = SYS_USER,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = SYS_USER,
                            ModifiedDate = DateTime.Now,
                            ObjectState = ObjectState.Added
                        },
                        new OrderDetail
                        {
                            MenuItemID = 2,
                            Quantity = 5,
                            CreatedBy = SYS_USER,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = SYS_USER,
                            ModifiedDate = DateTime.Now,
                            ObjectState = ObjectState.Added
                        }
                    }
                };

                orderRepository.InsertOrUpdateGraph(orderTest);

                try
                {
                    unitOfWork.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());

                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    Debug.WriteLine(sb.ToString());
                    TestContext.WriteLine(sb.ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    TestContext.WriteLine(ex.Message);
                }
            }
        }
    }
}