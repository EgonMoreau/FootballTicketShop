using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.ViewModels
{
    public class TeamVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }

        public Stadiums Stadium { get; set; }

    }

}
