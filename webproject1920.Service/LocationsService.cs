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
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsDAO _locationsDAO;

        public LocationsService(ILocationsDAO locationsDAO)
        {
            _locationsDAO = locationsDAO;
        }

        public Task<IEnumerable<Locations>> GetAll()
        {
            return _locationsDAO.GetAll();
        }
        public Task<Locations> GetById(int id)
        {
            return _locationsDAO.GetById(id);
        }
    }
}
