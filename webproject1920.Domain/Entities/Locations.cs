using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Locations
    {
        public Locations()
        {
            MatchStadiumLocation = new HashSet<MatchStadiumLocation>();
            StadiumLocations = new HashSet<StadiumLocations>();
            TeamCompetitionLocation = new HashSet<TeamCompetitionLocation>();
        }

        public int Id { get; set; }
        public string Location { get; set; }

        public virtual ICollection<MatchStadiumLocation> MatchStadiumLocation { get; set; }
        public virtual ICollection<StadiumLocations> StadiumLocations { get; set; }
        public virtual ICollection<TeamCompetitionLocation> TeamCompetitionLocation { get; set; }
    }
}
