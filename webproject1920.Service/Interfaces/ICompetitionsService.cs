using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface ICompetitionsService
    {
        Task<IEnumerable<Competitions>> GetAll();
        Task<Competitions> GetById(int competitionId);
        Task<Competitions> GetNextCompetition(DateTime time);
    }
}
