using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class StadiumLocations
    {
        public StadiumLocations()
        {
            MatchStadiumLocation = new HashSet<MatchStadiumLocation>();
        }

        public int Id { get; set; }
        public int StadiumId { get; set; }
        public int LocationId { get; set; }
        public int AvailableSeats { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Stadiums Stadium { get; set; }
        public virtual ICollection<MatchStadiumLocation> MatchStadiumLocation { get; set; }
    }
}
