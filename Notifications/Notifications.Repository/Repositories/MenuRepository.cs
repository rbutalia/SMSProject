
using System.Linq;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;

namespace Notifications.Repository.Repositories
{
    public static class MenuRepository
    { 
        public static Menu GetMenuByCompanyID(this IRepositoryAsync<Menu> repository, int companyID)
        {
                return repository
                        .Queryable()
                        .Where(x => x.CompanyID == companyID && x.IsActive).SingleOrDefault();
        }

        public static Menu GetMenuByCompanyIdentifier(this IRepositoryAsync<Menu> repository, string textIdentifier)
        {
            return repository
                    .Query(x => x.Company.TextIdentifier.ToUpper().Equals(textIdentifier) && x.IsActive)
                    .Include(x => x.MenuItems)
                    .Select()
                    .SingleOrDefault();
        }
    }
}
