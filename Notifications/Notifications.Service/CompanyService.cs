
using Service.Pattern;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;
using Notifications.Repository.Repositories;

namespace Notifications.Services
{
    public interface ICompanyService : IService<Company>
    {
        Company GetCompanyByTextIdentifier(string textIdentifier);
        Company GetCompanyByID(int companyID);
    }

    /// <summary>
    ///     All methods that are exposed from Repository in Service are overridable to add business logic,
    ///     business logic should be in the Service layer and not in repository for separation of concerns.
    /// </summary>
    public class CompanyService : Service<Company>, ICompanyService
    {
        private readonly IRepositoryAsync<Company> _repository;

        public CompanyService(IRepositoryAsync<Company> repository) : base(repository)
        {
            _repository = repository;
        }

        public Company GetCompanyByTextIdentifier(string textIdentifier)
        {
            return _repository.GetCompanyByTextIdentifier(textIdentifier);
        }

        public Company GetCompanyByID(int companyID)
        {
            return _repository.GetCompanyByID(companyID);
        }
    }

}
