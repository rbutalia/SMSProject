
using System.Text;
using Service.Pattern;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;
using Notifications.Repository.Repositories;

namespace Notifications.Services
{
    public interface IMenuService : IService<Menu>
    {
        Menu GetMenuByCompanyID(int companyID);
        string GetMenuByCompanyIdentifier(string textIdentifier);
        decimal GetMenuPriceByMenuItemID(int companyID, int menuItemID);
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class MenuService : Service<Menu>, IMenuService
    {
        private readonly IRepositoryAsync<Menu> _repository;

        public MenuService(IRepositoryAsync<Menu> repository) : base(repository)
        {
            _repository = repository;
        }
        public Menu GetMenuByCompanyID(int companyID)
        {
            return _repository.GetMenuByID(companyID);
        }
        public string GetMenuByCompanyIdentifier(string textIdentifier)
        {
            var menuBuilder = new StringBuilder();
            menuBuilder.AppendLine("Please reply with your choice, as under;");
            var thisMenu = _repository.GetMenuByCompanyIdentifier(textIdentifier);
            if (thisMenu == null) return string.Empty;
            foreach (MenuItem item in thisMenu.MenuItems)
            {
                menuBuilder.AppendLine(string.Format("{0}: {1}", item.MenuItemID, item.ItemName));
            }
            return menuBuilder.ToString();
        }
        public decimal GetMenuPriceByMenuItemID(int companyID, int menuItemID)
        {
            var menu = _repository.GetMenuByID(companyID);
            foreach (var menuItem in menu.MenuItems)
            {
                if (menuItem.MenuItemID == menuItemID)
                    return menuItem.Price;
            }
            return 0.0m;
        }
    }
}
