using System.ComponentModel;

namespace BikeTracker.Models
{
    public class TeamEditModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        [DisplayName("Rides in Reverse")]
        public bool ReverseRoute { get; set; }

        public string Image { get; set; }
    }
}