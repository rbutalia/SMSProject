
using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Repository.Pattern.Ef6;
using Notifications.Entities;
using Notifications.Entities.Models;
using Repository.Pattern.UnitOfWork;
using System.Data.Entity.Validation;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        public TestContext TestContext { get; set; }
        public const string TEST_USER = "Randeep Butalia";
        public const int TEST_COMPANY = 99;

        private IDataContextAsync _context;
        private IUnitOfWorkAsync _unitOfWork;
        IRepositoryAsync<Product> _productRepository;
        IRepositoryAsync<Category> _categoryRepository;
        IRepositoryAsync<Menu> _menuRepository;
        //IRepositoryAsync<MenuItem> _categoryRepository;

        [TestInitialize]
        public void Initialize()
        {
            //Utility.CreateSeededTestDatabase();
            _context = new NotificationsContext();
            _unitOfWork = new UnitOfWork(_context);
            _productRepository = new Repository<Product>(_context, _unitOfWork);
            _categoryRepository = new Repository<Category>(_context, _unitOfWork);
            _menuRepository = new Repository<Menu>(_context, _unitOfWork);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _menuRepository = null;
            _categoryRepository = null;
            _productRepository = null;
            _unitOfWork = null;
            _context = null;
        }

        [TestMethod]
        public void InsertProducts()
        {
            var cat1 = new Category { CategoryName = "Category 1", Description = "Example category 1", CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added };

            var newProducts = new[]
            {
                new Product {ProductName = "One", CategoryID=cat1.CategoryID, Discontinued = false, ObjectState = ObjectState.Added},
                new Product {ProductName = "123456789012345678234567890", CategoryID=cat1.CategoryID, Discontinued = true, ObjectState = ObjectState.Added},
                new Product {ProductName = "Three", CategoryID=cat1.CategoryID, Discontinued = true, ObjectState = ObjectState.Added},
                new Product {ProductName = "Four", CategoryID=cat1.CategoryID, Discontinued = true, ObjectState = ObjectState.Added},
                new Product {ProductName = "Five", CategoryID=cat1.CategoryID, Discontinued = true, ObjectState = ObjectState.Added}
            };

            try
            {
                _categoryRepository.Insert(cat1);
                _productRepository.InsertGraphRange(newProducts);
                _unitOfWork.SaveChanges();
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

            var insertedProduct = _productRepository.Query(x => x.ProductName == "One").Select().FirstOrDefault();
            Assert.IsTrue(insertedProduct.ProductName == "One");

            foreach (var product in newProducts)
            {
                _productRepository.Delete(product.ProductID);
            }
            _unitOfWork.SaveChanges();
            _categoryRepository.Delete(cat1.CategoryID);
            _unitOfWork.SaveChanges();
        }

        [TestMethod]
        public void InsertMenuItems()
        {
            var menu1 = new Menu { IsActive = true, CompanyID = TEST_COMPANY, MenuName = "Menu For DoughZone", CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added };

            var menuItems = new[]
            {
                new MenuItem { MenuID = menu1.MenuID, ItemName = "Item 1", IsActive = true, CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added},
                new MenuItem { MenuID = menu1.MenuID, ItemName = "Item 2", IsActive = true, CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added},
                new MenuItem { MenuID = menu1.MenuID, ItemName = "Item 3", IsActive = true, CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added},
                new MenuItem { MenuID = menu1.MenuID, ItemName = "Item 4", IsActive = true, CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added},
                new MenuItem { MenuID = menu1.MenuID, ItemName = "Item 5", IsActive = true, CreatedBy = TEST_USER, CreatedDate = DateTime.Now, ModifiedBy = TEST_USER, ModifiedDate = DateTime.Now, ObjectState = ObjectState.Added}
            };

            try
            {
                foreach (var item in menuItems)
                {
                    menu1.MenuItems.Add(item);
                }
                _menuRepository.InsertOrUpdateGraph(menu1);
                _unitOfWork.SaveChanges();
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

            foreach (var item in menuItems)
            {
                item.ObjectState = ObjectState.Deleted;
            }
            menu1.ObjectState = ObjectState.Deleted;
            _menuRepository.Delete(menu1.MenuID); 
            _unitOfWork.SaveChanges();
        }
    }
}