using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webproject1920.ViewModels
{
    public class NewOrderLineVM
    {
        // For ticket
        public int? MSL { get; set; }
        public int? Match { get; set; }

        // For subscription
        public int? TSL { get; set; }
        public int? Team { get; set; }

        public Guid Customer { get; set; }
    }
}
