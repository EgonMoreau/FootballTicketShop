using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Repository;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class MatchStadiumLocationService : IMatchStadiumLocationService
    {
        private readonly IMatchStadiumLocationDAO _matchStadiumLocationDAO;
        private readonly IMatchService _matchService;
        private readonly IStadiumLocationsService _slService;

        public MatchStadiumLocationService(IMatchStadiumLocationDAO matchStadiumLocationDAO, IStadiumLocationsService slService, IMatchService matchService)
        {
            _matchStadiumLocationDAO = matchStadiumLocationDAO;
            _matchService = matchService;
            _slService = slService;
        }

        public Task<IEnumerable<MatchStadiumLocation>> GetAll()
        {
            return _matchStadiumLocationDAO.GetAll();
        }

        public Task<MatchStadiumLocation> GetById(int id)
        {
            return _matchStadiumLocationDAO.GetById(id);
        }

        public Task<IEnumerable<MatchStadiumLocation>> GetByMatchId(int matchId)
        {
            return _matchStadiumLocationDAO.GetByMatchId(matchId);
        }

        public Task Update(MatchStadiumLocation entity)
        {
            return _matchStadiumLocationDAO.Update(entity);
        }
        public Task<IEnumerable<MatchStadiumLocation>> GetMslByMatches(IEnumerable<Match> matches)
        {
            return _matchStadiumLocationDAO.GetMslByMatches(matches);
        }
        
        //public Task GetByMatchTeamId(int id1, int id2)
        //{
        //    return _matchStadiumLocationDAO.Update(entity);
        //}
    }
}
