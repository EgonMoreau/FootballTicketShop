using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Repository;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionDAO _subscriptionDAO;

        public SubscriptionService(ISubscriptionDAO subscriptionDAO)
        {
            _subscriptionDAO = subscriptionDAO;
        }

        public Task<IEnumerable<Subscription>> GetAll()
        {
            return _subscriptionDAO.GetAll();
        }

        public Task Create(Subscription entity)
        {
            return _subscriptionDAO.Create(entity);
        }
    }
}
