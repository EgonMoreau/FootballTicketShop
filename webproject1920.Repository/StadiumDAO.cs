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
    public class StadiumDAO : IStadiumDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public StadiumDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Stadiums>> GetAll()
        {
            return await _dbContext.Stadiums.ToListAsync();
        }

        public async Task<Stadiums> GetById(int id) {
            return await _dbContext.Stadiums.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
