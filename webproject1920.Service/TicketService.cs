using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class TicketService : ITicketService
    {
        private readonly ITicketDAO _ticketDAO;

        public TicketService(ITicketDAO teamsDAO)
        {
            _ticketDAO = teamsDAO;
        }

        public Task<Ticket> GetById(int id)
        {
            return _ticketDAO.GetById(id);
        }

        public Task Create(Ticket entity)
        {
            return _ticketDAO.Create(entity);
        }

        public Task<IEnumerable<Ticket>> GetByCustomerAndMatch(Guid customer, int matchId)
        {
            return _ticketDAO.GetByCustomerAndMatch(customer, matchId);
        }

        public Task Update(Ticket entity)
        {
            return _ticketDAO.Update(entity);
        }
    }
}
