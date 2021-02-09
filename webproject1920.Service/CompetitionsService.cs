using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Domain.Interfaces;
using webproject1920.Repository;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class CompetitionsService : ICompetitionsService
    {
        private readonly ICompetitionsDAO _competitionsDAO;

        public CompetitionsService(ICompetitionsDAO competitionsDAO)
        {
            _competitionsDAO = competitionsDAO;
        }

        public Task<IEnumerable<Competitions>> GetAll()
        {
            return _competitionsDAO.GetAll();
        }
        
        public Task<Competitions> GetById(int competitionId)
        {
            return _competitionsDAO.GetById(competitionId);
        }

        public Task<Competitions> GetNextCompetition(DateTime time)
        {
            return _competitionsDAO.GetNextCompetition(time);
        }
    }
}
