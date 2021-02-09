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
    public class MatchStadiumLocationDAO : IMatchStadiumLocationDAO
    {
        private readonly IWebproject1920Context _dbContext;

        public MatchStadiumLocationDAO(IWebproject1920Context dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MatchStadiumLocation>> GetAll()
        {
            return await _dbContext.MatchStadiumLocation
                .Include(x => x.StadiumLocation)
                .Include(x => x.StadiumLocation.Location)
                .ToListAsync();
        }

        public async Task<MatchStadiumLocation> GetById(int id)
        {
            return await _dbContext.MatchStadiumLocation
                .Include(x => x.Match)
                .Include(x => x.Match.TeamAwayNavigation)
                .Include(x => x.Match.TeamHomeNavigation)
                .Include(x => x.StadiumLocation)
                .Include(x => x.StadiumLocation.Location)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<MatchStadiumLocation>> GetByMatchId(int matchId)
        {
            return await _dbContext.MatchStadiumLocation
                .Where(x => x.MatchId == matchId)
                .Include(x => x.StadiumLocation)
                .Include(x => x.StadiumLocation.Location)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<MatchStadiumLocation>> GetByMatchTeamId(int id1, int id2)
        {
            return await _dbContext.MatchStadiumLocation
                .Where(x => x.MatchId == id1)
                .Where(x => x.StadiumLocationId == id1)
                .Include(x => x.StadiumLocation)
                .Include(x => x.StadiumLocation.Location)
                .ToListAsync();
        }
        public async Task<IEnumerable<MatchStadiumLocation>> GetMslByMatches(IEnumerable<Match> matches)
        {
            return await _dbContext.MatchStadiumLocation
                .Where(x => matches.Any(m => m.Id == x.MatchId))
                .ToListAsync();
        }

        public async Task Update(MatchStadiumLocation entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
