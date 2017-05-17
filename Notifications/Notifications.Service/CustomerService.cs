
using Service.Pattern;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;
using Notifications.Repository.Repositories;

namespace Notifications.Services
{
    public interface ICustomerService : IService<Customer>
    {
        decimal CustomerOrderTotalByYear(int customerId, int year);
        IEnumerable<Customer> CustomersByCompanyId(int companyId);
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly IRepositoryAsync<Customer> _repository;

        public CustomerService(IRepositoryAsync<Customer> repository) : base(repository)
        {
            _repository = repository;
        }

        public decimal CustomerOrderTotalByYear(int customerId, int year)
        {
            return _repository.GetCustomerOrderTotalByYear(customerId, year);
        }

        public IEnumerable<Customer> CustomersByCompanyId(int companyId)
        {
            return _repository.CustomersByCompanyId(companyId);
        }
    }
}