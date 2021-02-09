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
    public class CompetitionsDAO : ICompetitionsDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public CompetitionsDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Competitions>> GetAll()
        {
            return await _dbContext.Competitions.ToListAsync();
        }
        
        public async Task<Competitions> GetNextCompetition(DateTime time)
        {
            return await _dbContext.Competitions
                .Where(x => time < x.StartDate)
                .FirstAsync();
        }

        public async Task<Competitions> GetById(int competitionId)
        {
            return await _dbContext.Competitions
                .FirstOrDefaultAsync(x => x.Id == competitionId);
        }
    }
}
