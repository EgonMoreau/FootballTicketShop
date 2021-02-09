using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using webproject1920.Service.Interfaces;
using webproject1920.Domain.Interfaces;

namespace webproject1920.Repository
{

    public class TeamsDAO : ITeamsDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public TeamsDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Teams>> GetAll()
        {
            return await _dbContext.Teams
                .Include(x => x.Stadium)
                .ToListAsync();
        }
        
        public async Task<Teams> GetById(int id)
        {
            return await _dbContext.Teams
                .Where(x => x.Id == id)
                .Include(x => x.Stadium)
                .FirstOrDefaultAsync();
        }


    }
}
