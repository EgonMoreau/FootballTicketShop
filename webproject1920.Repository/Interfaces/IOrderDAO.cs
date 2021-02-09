using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IOrderDAO
    {
        Task<IEnumerable<Order>> GetAll();
        Task Create(Order entity);
        Task<IEnumerable<Order>> GetByCustomer(Guid customer);
        Task<Order> GetById(int id);
    }
}
