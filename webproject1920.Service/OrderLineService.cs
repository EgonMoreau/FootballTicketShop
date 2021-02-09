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
    public class OrderLineService : IOrderLineService
    {
        private readonly IOrderLineDAO _orderLineDAO;

        public OrderLineService(IOrderLineDAO orderLineDAO)
        {
            _orderLineDAO = orderLineDAO;
        }

        public Task<IEnumerable<OrderLine>> GetAll()
        {
            return _orderLineDAO.GetAll();
        }

        public Task Create(OrderLine entity)
        {
            return _orderLineDAO.Create(entity);
        }

        public Task<IEnumerable<OrderLine>> GetByOrderId(int id)
        {
            return _orderLineDAO.GetByOrderId(id);
        }

        public Task<OrderLine> GetById(int id)
        {
            return _orderLineDAO.GetById(id);
        }

        public Task Update(OrderLine entity)
        {
            return _orderLineDAO.Update(entity);
        }
    }
}
