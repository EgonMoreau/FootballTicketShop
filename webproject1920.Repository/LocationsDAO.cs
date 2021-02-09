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
    public class LocationsDAO : ILocationsDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public LocationsDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Locations>> GetAll()
        {
            return await _dbContext.Locations.ToListAsync();
        }

        public async Task<Locations> GetById(int id)
        {
            return await _dbContext.Locations
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
