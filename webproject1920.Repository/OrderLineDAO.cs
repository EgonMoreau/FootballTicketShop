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
    public class OrderLineDAO : IOrderLineDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public OrderLineDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderLine>> GetAll()
        {
            return await _dbContext.OrderLine.ToListAsync();
        }

        public async Task Create(OrderLine entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderLine>> GetByOrderId(int id)
        {
            return await _dbContext.OrderLine
                .Where(x => x.OrderId == id)
                .Include(x => x.Subscription)
                .Include(x => x.Subscription.TeamCompetitionLocation)
                .Include(x => x.Subscription.TeamCompetitionLocation.Team)
                .Include(x => x.Subscription.TeamCompetitionLocation.Location)
                .Include(x => x.Subscription.TeamCompetitionLocation.Competition)
                .Include(x => x.Ticket)
                .Include(x => x.Ticket.Match)
                .Include(x => x.Ticket.Match.TeamHomeNavigation)
                .Include(x => x.Ticket.Match.TeamAwayNavigation)
                .Include(x => x.Ticket.MatchStadiumLocation)
                .Include(x => x.Ticket.MatchStadiumLocation.Location)
                .ToListAsync();
        }

        public async Task<OrderLine> GetById(int id)
        {
            return await _dbContext.OrderLine
                .Where(x => x.Id == id)
                .Include(x => x.Order)
                .Include(x => x.Ticket)
                .Include(x => x.Ticket.Match)
                .FirstOrDefaultAsync();
        }

        public async Task Update(OrderLine entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
