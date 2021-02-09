using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IStadiumLocationsService
    {
        Task<IEnumerable<StadiumLocations>> GetAll();
        Task<IEnumerable<StadiumLocations>> GetByStadiumId(int stadiumId);
        Task<StadiumLocations> GetByStadiumLocationId(int stadiumId, int locationId);
    }
}
