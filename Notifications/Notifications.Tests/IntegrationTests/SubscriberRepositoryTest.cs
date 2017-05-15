using System;
using System.Text;
using System.Diagnostics;
using Repository.Pattern.Ef6;
using Notifications.Entities;
using Repository.Pattern.UnitOfWork;
using Notifications.Entities.Models;
using System.Data.Entity.Validation;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Notifications.Tests.IntegrationTests
{
    [TestClass]
    public class SubscriberRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            //Utility.CreateSeededTestDatabase();
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void InsertSubscribers()
        {
            using (IDataContextAsync context = new NotificationsContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Subscriber> subscriberRepository = new Repository<Subscriber>(context, unitOfWork);
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                IRepositoryAsync<Company> companyRepository = new Repository<Company>(context, unitOfWork);

                var newCompany = new Company { CompanyName = "DoughZone", ContactPersonName = "Edwin", ObjectState = ObjectState.Added };
                var newCustomers = new[]
                {
                    new Customer { CompanyID = newCompany.CompanyID, ContactName = "Randeep Butalia", StreetAddress_Line1="Address Line 1", ContactTitle="Mr.", StreetAddress_Line2="", City="Ft. Erie", Region="Ontario", Country="CA", PostalCode="L2A 1P7", Phone="+17165414925", Fax="+19052324584", ObjectState = ObjectState.Added },
                    new Customer { CompanyID = newCompany.CompanyID, ContactName = "Manit Butalia", StreetAddress_Line1="Address Line 1", ContactTitle="Mr.", StreetAddress_Line2="", City="Toronto", Region="Ontario", Country="CA", PostalCode="L2H 1S7", Phone="+16472324584", Fax="", ObjectState = ObjectState.Added }
                    //new Product {ProductName = "Three", Discontinued = true, ObjectState = ObjectState.Added},
                    //new Product {ProductName = "Four", Discontinued = true, ObjectState = ObjectState.Added},
                    //new Product {ProductName = "Five", Discontinued = true, ObjectState = ObjectState.Added}
                };

                
                //unitOfWork.SaveChanges();

                var newSubscribers = new[]
                {
                    new Subscriber {CustomerId = 3, PhoneNumber = "+17165414925", IsActive = true, Subscribed=true, CreatedOn=DateTime.Now, UpdatedOn=DateTime.Now, ObjectState = ObjectState.Added },
                    new Subscriber {CustomerId = 4, PhoneNumber = "+16472422345", IsActive = true, Subscribed=true, CreatedOn=DateTime.Now, UpdatedOn=DateTime.Now, ObjectState = ObjectState.Added }
                    //new Product {ProductName = "Three", Discontinued = true, ObjectState = ObjectState.Added},
                    //new Product {ProductName = "Four", Discontinued = true, ObjectState = ObjectState.Added},
                    //new Product {ProductName = "Five", Discontinued = true, ObjectState = ObjectState.Added}
                };
                
                //foreach (var sub in newSubscribers)
                {
                    try
                    {
                        companyRepository.Insert(newCompany);
                        customerRepository.InsertGraphRange(newCustomers);
                        //subscriberRepository.InsertRange(newSubscribers);
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

                var insertedSubscriber = subscriberRepository.Query(x => x.CustomerId == 1).Select();
                //Assert.IsTrue(insertedSubscriber.Cu == "One");
            }
        }
    }
}
