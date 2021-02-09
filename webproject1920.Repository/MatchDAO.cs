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
    public class MatchDAO : IMatchDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public MatchDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Match>> GetAll(DateTime start, DateTime end)
        {
            return await _dbContext.Match
                .Include(x => x.Stadium)
                .Include(x => x.TeamAwayNavigation)
                .Include(x => x.TeamHomeNavigation)
                .Where(x => x.Start >= start && x.Start <= end)
                .ToListAsync();
        }

        public async Task<IEnumerable<Match>> GetByCompetitionTeamId(int idComp, int idTeam)
        {
            return await _dbContext.Match
                .Include(x => x.Stadium)
                .Include(x => x.TeamAwayNavigation)
                .Include(x => x.TeamHomeNavigation)
                .Where(x => x.CompetitionId == idComp)
                .Where(x => x.TeamHome == idTeam || x.TeamAway == idTeam)
                .ToListAsync();
        }

        public async Task<Match> GetById(int id)
        {
            return await _dbContext.Match
                .Include(x => x.Stadium)
                .Include(x => x.TeamAwayNavigation)
                .Include(x => x.TeamHomeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Match>> GetByTeamId(DateTime start, DateTime end, int teamId)
        {
            return await _dbContext.Match
                .Include(x => x.Stadium)
                .Include(x => x.TeamAwayNavigation)
                .Include(x => x.TeamHomeNavigation)
                .Where(x => x.Start >= start && x.Start <= end)
                .Where(x => x.TeamAway.Equals(teamId) || x.TeamHome.Equals(teamId))
                .ToListAsync();
        }
    }
}
