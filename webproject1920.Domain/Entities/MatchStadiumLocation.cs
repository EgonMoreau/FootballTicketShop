using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class MatchStadiumLocation
    {
        public MatchStadiumLocation()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int MatchId { get; set; }
        public int StadiumLocationId { get; set; }
        public int RemainingSeats { get; set; }
        public float Price { get; set; }
        public int LocationId { get; set; }

        public virtual Locations Location { get; set; }
        public virtual Match Match { get; set; }
        public virtual StadiumLocations StadiumLocation { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
