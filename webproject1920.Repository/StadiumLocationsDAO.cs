using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Service.Interfaces;

namespace webproject1920.Repository
{
    public class StadiumLocationsDAO : IStadiumLocationsDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public StadiumLocationsDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<StadiumLocations>> GetAll()
        {
            return await _dbContext.StadiumLocations
                .Include(x => x.Location)
                .ToListAsync();
        }

        public async Task<IEnumerable<StadiumLocations>> GetByStadiumId(int stadiumId)
        {
            return await _dbContext.StadiumLocations
                .Where(x => x.StadiumId == stadiumId)
                .Include(x => x.Location)
                .ToListAsync();
        }
        
        public async Task<StadiumLocations> GetByStadiumLocationId(int stadiumId, int locationId)
        {
            return await _dbContext.StadiumLocations
                .Where(x => x.StadiumId == stadiumId)
                .Where(x => x.LocationId == locationId)
                .Include(x => x.Location)
                .FirstOrDefaultAsync();
        }
    }
}
