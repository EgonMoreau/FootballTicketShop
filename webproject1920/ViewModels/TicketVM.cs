using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class TicketVM
    {
        public int Id { get; set; }
        public int MatchStadiumLocationId { get; set; }
        public Guid Code { get; set; }
        public int MatchId { get; set; }
    }
}
