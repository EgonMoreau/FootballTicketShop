using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Repository.Interfaces;

namespace webproject1920.Repository
{
    public class TeamCompetitionLocationDAO : ITeamCompetitionLocationDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public TeamCompetitionLocationDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<IEnumerable<TeamCompetitionLocation>> GetAll()
        {
            return await _dbContext.TeamCompetitionLocation.ToListAsync();
        }

        public async Task<TeamCompetitionLocation> GetById(int id)
        {
            return await _dbContext.TeamCompetitionLocation
                .Include(x => x.Location)
                .Include(x => x.Team)
                .Include(x => x.Competition)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<IEnumerable<TeamCompetitionLocation>> GetByTeamCompetitionId(int teamId, int competitionId)
        {
            return await _dbContext.TeamCompetitionLocation
                .Where(x => x.TeamId == teamId)
                .Where(x => x.CompetitionId == competitionId)
                .Include(x => x.Location)
                .ToListAsync();
        }
    }
}
