using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Repository;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDAO _orderDAO;

        public OrderService(IOrderDAO orderDAO)
        {
            _orderDAO = orderDAO;
        }

        public Task<IEnumerable<Order>> GetAll()
        {
            return _orderDAO.GetAll();
        }

        public async Task Create(Order entity)
        {
            entity.OrderDate = DateTime.Now;
            await _orderDAO.Create(entity);
        }

        public Task<Order> GetById(int id)
        {
            return _orderDAO.GetById(id);
        }

        public Task<IEnumerable<Order>> GetByCustomer(Guid customer)
        {
            return _orderDAO.GetByCustomer(customer);
        }
    }
}
