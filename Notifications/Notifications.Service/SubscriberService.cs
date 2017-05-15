
using Service.Pattern;
using System.Collections.Generic;
using Notifications.Entities.Models;
using Repository.Pattern.Repositories;
using Notifications.Repository.Repositories;

namespace Notifications.Services
{
    public interface ISubscriberService: IService<Subscriber>
    {
        IEnumerable<Subscriber> SubscribersByCompanyID(int companyID);
        IEnumerable<Subscriber> ActiveSubscribersByCompanyID(int companyID);
        Subscriber FindByPhoneNumber(string phoneNumber);
    }

    public class SubscriberService : Service<Subscriber>, ISubscriberService
    {
        private readonly IRepositoryAsync<Subscriber> _repository;

        public SubscriberService(IRepositoryAsync<Subscriber> repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Subscriber> SubscribersByCompanyID(int companyID)
        {
            return _repository.SubscribersByCompanyID(companyID);
        }
        public IEnumerable<Subscriber> ActiveSubscribersByCompanyID(int companyID)
        {
            return _repository.ActiveSubscribersByCompanyID(companyID);
        }
        public Subscriber FindByPhoneNumber(string phoneNumber)
        {
            return _repository.FindByPhoneNumber(phoneNumber);
        }
    }
}
