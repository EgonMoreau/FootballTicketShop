using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Teams
    {
        public Teams()
        {
            MatchTeamAwayNavigation = new HashSet<Match>();
            MatchTeamHomeNavigation = new HashSet<Match>();
            TeamCompetitionLocation = new HashSet<TeamCompetitionLocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StadiumId { get; set; }
        public string Logo { get; set; }
        public string Image { get; set; }

        public virtual Stadiums Stadium { get; set; }
        public virtual ICollection<Match> MatchTeamAwayNavigation { get; set; }
        public virtual ICollection<Match> MatchTeamHomeNavigation { get; set; }
        public virtual ICollection<TeamCompetitionLocation> TeamCompetitionLocation { get; set; }
    }
}
