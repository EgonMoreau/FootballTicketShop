using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Service.Interfaces;

namespace webproject1920.Repository
{
    public class OrderDAO : IOrderDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public OrderDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _dbContext.Order.ToListAsync();
        }

        public async Task Create(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomer(Guid customer)
        {
            return await _dbContext.Order.Where(x => x.Customer == customer).ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _dbContext.Order.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
