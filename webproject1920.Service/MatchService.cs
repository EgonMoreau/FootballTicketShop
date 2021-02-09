using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class MatchService : IMatchService
    {
        private readonly IMatchDAO _matchDAO;

        public MatchService(IMatchDAO matchDAO)
        {
            _matchDAO = matchDAO;
        }

        public Task<IEnumerable<Match>> GetAll(DateTime start, DateTime end)
        {
            return _matchDAO.GetAll(start, end);
        }

        public Task<IEnumerable<Match>> GetByCompetitionTeamId(int idComp, int idTeam)
        {
            return _matchDAO.GetByCompetitionTeamId(idComp, idTeam);
        }

        public Task<Match> GetById(int id)
        {
            return _matchDAO.GetById(id);
        }

        public Task<IEnumerable<Match>> GetByTeamId(DateTime start, DateTime end, int id)
        {
            return _matchDAO.GetByTeamId(start, end, id);
        }

        
    }
}
