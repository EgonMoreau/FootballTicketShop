using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Ticket
    {
        public Ticket()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public int MatchStadiumLocationId { get; set; }
        public Guid Code { get; set; }
        public Guid Customer { get; set; }
        public int MatchId { get; set; }
        public bool? Valid { get; set; }

        public virtual Match Match { get; set; }
        public virtual MatchStadiumLocation MatchStadiumLocation { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
