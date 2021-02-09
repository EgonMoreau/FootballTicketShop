using System.Collections.Generic;
using webproject1920.Domain.Entities;

namespace webproject1920.ViewModels
{
    public class ShopTicketInfoVM
    {
        public List<MSLVM> MSLs { get; set; }
        public MSLVM SelectedMSL { get; set; }
        public MatchVM Match { get; set; }
        public StadiumVM Stadium { get; set; }
    }
}
