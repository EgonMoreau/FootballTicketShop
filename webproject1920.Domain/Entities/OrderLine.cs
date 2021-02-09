using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class OrderLine
    {
        public int OrderId { get; set; }
        public int? TicketId { get; set; }
        public int? SubscriptionId { get; set; }
        public int Id { get; set; }
        public float? Price { get; set; }
        public DateTime? ReturnedAt { get; set; }

        public virtual Order Order { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
