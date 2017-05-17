﻿
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
    }
}