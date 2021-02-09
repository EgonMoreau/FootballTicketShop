using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Stadiums
    {
        public Stadiums()
        {
            Match = new HashSet<Match>();
            StadiumLocations = new HashSet<StadiumLocations>();
            Teams = new HashSet<Teams>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public decimal? GeoLat { get; set; }
        public decimal? GeoLong { get; set; }

        public virtual ICollection<Match> Match { get; set; }
        public virtual ICollection<StadiumLocations> StadiumLocations { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
    }
}
