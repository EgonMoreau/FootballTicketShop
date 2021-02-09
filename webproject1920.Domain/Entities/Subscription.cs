using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Subscription
    {
        public Subscription()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int TeamCompetitionLocationId { get; set; }
        public Guid Code { get; set; }
        public Guid Customer { get; set; }

        public virtual TeamCompetitionLocation TeamCompetitionLocation { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
