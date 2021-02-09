using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IMatchStadiumLocationDAO
    {
        Task<IEnumerable<MatchStadiumLocation>> GetAll();
        Task<MatchStadiumLocation> GetById(int id);
        Task<IEnumerable<MatchStadiumLocation>> GetByMatchId(int id);
        Task<IEnumerable<MatchStadiumLocation>> GetByMatchTeamId(int id1, int id2);
        Task<IEnumerable<MatchStadiumLocation>> GetMslByMatches(IEnumerable<Match> matches);
        Task Update(MatchStadiumLocation entity);
    }
}
