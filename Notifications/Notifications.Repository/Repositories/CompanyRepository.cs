
using System.Linq;
using Repository.Pattern.Repositories;
using Notifications.Entities.Models;

namespace Notifications.Repository.Repositories
{
    public static class CompanyRepository
    {
        public static Company GetCompanyByTextIdentifier(this IRepository<Company> repository, string textIdentifier)
        {
           return repository
                    .Query(x => x.TextIdentifier.Equals(textIdentifier.ToUpper()))
                    .Include(x => x.WorkFlowSteps)
                    .Include(x => x.Menus)
                    .Select()
                    .SingleOrDefault();
        }
        public static Company GetCompanyByID(this IRepository<Company> repository, int companyID)
        {
            return repository
                     .Query(x => x.CompanyID == companyID)
                     .Include(x => x.WorkFlowSteps)
                     .Include(x => x.Menus)
                     .Select()
                     .SingleOrDefault();
        }
    }
}