using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task Create(Order entity);
        Task<Order> GetById(int id);
        Task<IEnumerable<Order>> GetByCustomer(Guid customer);
    }
}
