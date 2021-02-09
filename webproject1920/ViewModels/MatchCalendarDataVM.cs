using System;

namespace webproject1920.ViewModels
{
    public class MatchCalendarDataVM
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        public string StadiumName { get; set; }
        public string TeamAwayName { get; set; }
        public string TeamHomeName { get; set; }

        public string Start
        {
            get
            {
                return StartTime.ToString("u").Replace(" ", "T");
            }
        }
        public string End
        {
            get
            {
                return StartTime.AddHours(2).ToString("u").Replace(" ", "T");
            }
        }
        public string Title
        {
            get
            {
                return TeamHomeName + " - " + TeamAwayName;
            }
        }

    }
}