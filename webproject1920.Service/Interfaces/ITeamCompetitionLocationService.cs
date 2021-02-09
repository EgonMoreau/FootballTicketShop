using System.Collections.Generic;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface ITeamCompetitionLocationService
    {
        Task<IEnumerable<TeamCompetitionLocation>> GetAll();
        Task<TeamCompetitionLocation> GetById(int id);
        Task<IEnumerable<TeamCompetitionLocation>> GetByTeamCompetitionId(int teamId, int competitionId);
    }
}
