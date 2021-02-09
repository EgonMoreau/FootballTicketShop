using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Competitions
    {
        public Competitions()
        {
            Match = new HashSet<Match>();
            TeamCompetitionLocation = new HashSet<TeamCompetitionLocation>();
        }

        public int Id { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Match> Match { get; set; }
        public virtual ICollection<TeamCompetitionLocation> TeamCompetitionLocation { get; set; }
    }
}
