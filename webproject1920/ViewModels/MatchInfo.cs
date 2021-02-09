using System.Collections.Generic;

namespace webproject1920.ViewModels
{
    public class MatchInfo
    {
        public List<MSLVM> MSL { get; set; }
        public MatchVM Match { get; set; }
        public StadiumVM Stadium { get; set; }
    }
}
