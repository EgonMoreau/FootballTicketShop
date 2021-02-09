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
    public class StadiumLocationsService : IStadiumLocationsService
    {
        private readonly IStadiumLocationsDAO _stadiumLocationsDAO;

        public StadiumLocationsService(IStadiumLocationsDAO stadiumLocationsDAO)
        {
            _stadiumLocationsDAO = stadiumLocationsDAO;
        }

        public Task<IEnumerable<StadiumLocations>> GetAll()
        {
            return _stadiumLocationsDAO.GetAll();
        }

        public Task<IEnumerable<StadiumLocations>> GetByStadiumId(int stadiumId)
        {
            return _stadiumLocationsDAO.GetByStadiumId(stadiumId);
        }
        
        public Task<StadiumLocations> GetByStadiumLocationId(int stadiumId, int locationId)
        {
            return _stadiumLocationsDAO.GetByStadiumLocationId(stadiumId, locationId);
        }
    }
}
