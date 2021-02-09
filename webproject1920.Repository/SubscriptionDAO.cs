using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Service.Interfaces;

namespace webproject1920.Repository
{
    public class SubscriptionDAO : ISubscriptionDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public SubscriptionDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Subscription>> GetAll()
        {
            return await _dbContext.Subscription.ToListAsync();
        }

        public async Task Create(Subscription entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }
    }
}
