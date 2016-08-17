using System.ComponentModel;

namespace BikeTracker.Models
{
    public class TeamViewModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        [DisplayName("Rides in reverse")]
        public bool ReverseRoute { get; set; }

        [DisplayName("Current Distance")]
        public decimal CurrentDistance { get; set; }

        [DisplayName("Tracks Completed")]
        public int TracksCompleted { get; set; }
    }
}