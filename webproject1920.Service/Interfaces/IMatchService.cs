using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IMatchService
    {
        Task<IEnumerable<Match>> GetAll(DateTime start, DateTime end);

        Task<Match> GetById(int id);

        Task<IEnumerable<Match>> GetByTeamId(DateTime start, DateTime end, int id);
        Task<IEnumerable<Match>> GetByCompetitionTeamId(int idComp, int idTeam);

    }
}
