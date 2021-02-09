using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface ITicketService
    {
        Task Create(Ticket entity);
        Task<Ticket> GetById(int id);
        Task<IEnumerable<Ticket>> GetByCustomerAndMatch(Guid customer, int matchId);
        Task Update(Ticket entity);
    }
}
