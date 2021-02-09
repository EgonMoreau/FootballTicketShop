using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webproject1920.ViewModels
{
    public class StadiumLocationVM
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public int AvailableSeats { get; set; }
    }
}
