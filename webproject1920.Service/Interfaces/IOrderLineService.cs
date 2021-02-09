using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IOrderLineService
    {
        Task<IEnumerable<OrderLine>> GetAll();
        Task Create(OrderLine entity);
        Task<IEnumerable<OrderLine>> GetByOrderId(int id);
        Task<OrderLine> GetById(int id);
        Task Update(OrderLine entity);
    }
}
