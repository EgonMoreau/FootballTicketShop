using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class TeamCompetitionLocation
    {
        public TeamCompetitionLocation()
        {
            Subscription = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public int TeamId { get; set; }
        public int CompetitionId { get; set; }
        public int LocationId { get; set; }
        public float Price { get; set; }

        public virtual Competitions Competition { get; set; }
        public virtual Locations Location { get; set; }
        public virtual Teams Team { get; set; }
        public virtual ICollection<Subscription> Subscription { get; set; }
    }
}
