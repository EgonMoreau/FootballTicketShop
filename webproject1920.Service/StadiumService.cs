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
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumDAO _stadionDAO;

        public StadiumService(IStadiumDAO stadionDAO)
        {
            _stadionDAO = stadionDAO;
        }

        public Task<IEnumerable<Stadiums>> GetAll()
        {
            return _stadionDAO.GetAll();
        }

        public Task<Stadiums> GetById(int id) {
            return _stadionDAO.GetById(id);
        }
    }
}
