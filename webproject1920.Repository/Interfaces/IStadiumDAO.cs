using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.Service.Interfaces
{
    public interface IStadiumDAO
    {
        Task<IEnumerable<Stadiums>> GetAll();
        Task<Stadiums> GetById(int id);
    }
}
