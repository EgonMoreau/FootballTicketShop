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
    public class TicketDAO : ITicketDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public TicketDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _dbContext.Ticket.ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByCustomerAndMatch(Guid customer, int matchId)
        {
            return await _dbContext.Ticket.Where(x => x.Customer == customer && x.MatchId == matchId).ToListAsync();
        }

        public async Task Update(Ticket entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Ticket> GetById(int id)
        {
            return await _dbContext.Ticket.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
