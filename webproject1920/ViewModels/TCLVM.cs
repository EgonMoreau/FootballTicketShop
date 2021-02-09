using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webproject1920.Domain.Entities;

namespace webproject1920.ViewModels
{
    public class TCLVM
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int CompetitionId { get; set; }
        public int LocationId { get; set; }
        public float Price { get; set; }

        public virtual Competitions Competition { get; set; }
        public virtual Locations Location { get; set; }
        public virtual Teams Team { get; set; }
    }
}
