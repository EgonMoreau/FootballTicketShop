using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Repository.Interfaces;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class TeamCompetitionLocationService : ITeamCompetitionLocationService
    {
        private readonly ITeamCompetitionLocationDAO _teamCompetitionLocationDao;

        public TeamCompetitionLocationService(ITeamCompetitionLocationDAO teamCompetitionLocationDao)
        {
            _teamCompetitionLocationDao = teamCompetitionLocationDao;
        }
        public Task<IEnumerable<TeamCompetitionLocation>> GetAll()
        {
            return _teamCompetitionLocationDao.GetAll();
        }
        public Task<TeamCompetitionLocation> GetById(int id)
        {
            return _teamCompetitionLocationDao.GetById(id);
        }

        public Task<IEnumerable<TeamCompetitionLocation>> GetByTeamCompetitionId(int teamId, int competitionId)
        {
            return _teamCompetitionLocationDao.GetByTeamCompetitionId(teamId, competitionId);
        }
    }
}
