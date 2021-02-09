using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Repository.Interfaces
{
    public interface ITeamCompetitionLocationDAO
    {
        Task<IEnumerable<TeamCompetitionLocation>> GetAll();
        Task<TeamCompetitionLocation> GetById(int id);
        Task<IEnumerable<TeamCompetitionLocation>> GetByTeamCompetitionId(int teamId, int competitionId);
    }
}
