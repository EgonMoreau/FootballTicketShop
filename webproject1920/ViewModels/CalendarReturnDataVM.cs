
using System.ComponentModel.DataAnnotations;

namespace webproject1920.ViewModels
{
    public class CalendarReturnDataVM
    {
        public int? TeamId { get; set; }
        
        [Required]
        public string Start { get; set; }
        
        [Required]
        public string End { get; set; }
    }
}