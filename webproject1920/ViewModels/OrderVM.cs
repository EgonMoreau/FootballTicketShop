using System;
using System.Collections.Generic;

namespace webproject1920.Domain.Entities
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
