using webproject1920.Domain.Entities;

namespace webproject1920.ViewModels
{
    public class MSLVM
    {
        public int Id { get; set; }
        public int StadiumLocationId { get; set; }
        public int RemainingSeats { get; set; }
        public float Price { get; set; }

        public StadiumLocations StadiumLocation { get; set; }
        public Match Match { get; set; }
    }
}
