using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webproject1920.ViewModels
{
    public class MatchCalendarVM
    {
        public int? TeamId { get; set; }
        public TeamVM Team { get; set; }
        public DateTime? Date { get; set; }
    }
}
