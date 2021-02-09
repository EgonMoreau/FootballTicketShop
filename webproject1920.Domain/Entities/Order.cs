using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid Customer { get; set; }
        public float? TotalPrice { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
