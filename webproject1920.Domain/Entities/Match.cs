using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Match
    {
        public Match()
        {
            MatchStadiumLocation = new HashSet<MatchStadiumLocation>();
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public int StadiumId { get; set; }
        public int TeamAway { get; set; }
        public int TeamHome { get; set; }
        public int CompetitionId { get; set; }

        public virtual Competitions Competition { get; set; }
        public virtual Stadiums Stadium { get; set; }
        public virtual Teams TeamAwayNavigation { get; set; }
        public virtual Teams TeamHomeNavigation { get; set; }
        public virtual ICollection<MatchStadiumLocation> MatchStadiumLocation { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
