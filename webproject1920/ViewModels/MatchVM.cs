using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.ViewModels
{
    public class MatchVM
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public Stadiums Stadium { get; set; }
        public Teams TeamAway { get; set; }
        public Teams TeamHome { get; set; }
    }
}
