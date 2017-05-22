
using System.Web.Http;
using Notifications.Services;
using Notifications.Entities;
using Repository.Pattern.Ef6;
using Microsoft.Practices.Unity;
using Notifications.Entities.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Notifications.Entities.StoredProcedures;

namespace Notifications.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new UnityContainer();
            container
                .RegisterType<IDataContextAsync, NotificationsContext>(new PerRequestLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
                .RegisterType<IRepositoryAsync<Company>, Repository<Company>>()
                .RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>()
                .RegisterType<IRepositoryAsync<Product>, Repository<Product>>()
                .RegisterType<IRepositoryAsync<Subscriber>, Repository<Subscriber>>()
                .RegisterType<IRepositoryAsync<Menu>, Repository<Menu>>()
                .RegisterType<IRepositoryAsync<Order>, Repository<Order>>()
                .RegisterType<IOrderService, OrderService>()
                .RegisterType<IMenuService, MenuService>()
                .RegisterType<ICustomerService, CustomerService>()
                .RegisterType<ISubscriberService, SubscriberService>()
                .RegisterType<INotificationService, NotificationService>()
                .RegisterType<ICompanyService, CompanyService>()
                .RegisterType<INotificationStoredProcedures, NotificationsContext>(new PerRequestLifetimeManager())
                .RegisterType<IStoredProcedureService, StoredProcedureService>();
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}