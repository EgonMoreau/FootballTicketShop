using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface ITicketDAO
    {
        Task Create(Ticket entity);
        Task<Ticket> GetById(int id);
        Task<IEnumerable<Ticket>> GetByCustomerAndMatch(Guid customer, int matchId);
        Task Update(Ticket entity);
    }
}
