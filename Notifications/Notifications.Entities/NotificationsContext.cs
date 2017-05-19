
using System.Data.Entity;
using Repository.Pattern.Ef6;
using Notifications.Entities.Models;
using Notifications.Entities.Mappings;

namespace Notifications.Entities
{
    public partial class NotificationsContext: DataContext
    {
        static NotificationsContext(){
            Database.SetInitializer<NotificationsContext>(new DropCreateDatabaseIfModelChanges<NotificationsContext>());
        }

        public NotificationsContext(): base("Name=NotificationsContext"){
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<WorkflowStep> WorkflowSteps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CompanyConfig());
            modelBuilder.Configurations.Add(new CustomerConfig());
            modelBuilder.Configurations.Add(new ProductConfig());
            modelBuilder.Configurations.Add(new OrderDetailConfig());
            modelBuilder.Configurations.Add(new OrderConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new SupplierConfig());
            modelBuilder.Configurations.Add(new ShipperConfig());
            modelBuilder.Configurations.Add(new SubscriberConfig());
            modelBuilder.Configurations.Add(new MenuConfig());
            modelBuilder.Configurations.Add(new MenuItemConfig());
            modelBuilder.Configurations.Add(new WorkflowStepConfig());
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Notifications.Entities.Models.Company> Companies { get; set; }
    }
}
