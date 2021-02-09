using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public class OrderLineVM
    {        
        public int Id { get; set; }
        public int OrderId { get; set; }

        public float? Price { get; set; }

        public int? TicketId { get; set; }
        public Ticket Ticket { get; set;}

        public int? SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }

        public DateTime? ReturnedAt { get; set; }
    }
}
