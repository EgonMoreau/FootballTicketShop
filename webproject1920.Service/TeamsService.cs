using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;
using webproject1920.Repository;
using webproject1920.Service.Interfaces;

namespace webproject1920.Service
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamsDAO _teamsDAO;

        public TeamsService(ITeamsDAO teamsDAO)
        {
            _teamsDAO = teamsDAO;
        }

        public Task<IEnumerable<Teams>> GetAll()
        {
            return _teamsDAO.GetAll();
        }

        public Task<Teams> GetById(int id)
        {
            return _teamsDAO.GetById(id);
        }


    }
}
