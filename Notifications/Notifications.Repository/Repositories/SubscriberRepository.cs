
using System.Linq;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;

namespace Notifications.Repository.Repositories
{
    public static class SubscriberRepository
    {
        public static IEnumerable<Subscriber> SubscribersByCompanyID(this IRepositoryAsync<Subscriber> repository, int companyID)
        {
            return repository
                    .Queryable()
                    .Where(x => x.Customer.CompanyID == companyID)
                    .AsEnumerable();
        }

        public static IEnumerable<Subscriber> ActiveSubscribersByCompanyID(this IRepositoryAsync<Subscriber> repository, int companyID)
        {
            return repository
                    .Queryable()
                    .Where(x => x.Customer.CompanyID == companyID && x.IsActive)
                    .AsEnumerable();
        }

        public static Subscriber FindByPhoneNumber(this IRepositoryAsync<Subscriber> repository, string phoneNumber)
        {
            return repository
                    .Queryable()
                    .Where(x => x.PhoneNumber.Equals(phoneNumber))
                    .SingleOrDefault();
        }
    }
}
