using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webproject1920.ViewModels
{
    public class CompetitionVM
    {
        public int Id { get; set; }
        public string Season { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
