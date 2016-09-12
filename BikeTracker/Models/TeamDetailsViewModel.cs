using System.Collections.Generic;
using System.ComponentModel;

namespace BikeTracker.Models
{
    public class TeamDetailsViewModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        [DisplayName("Rides in Reverse")]
        public bool ReverseRoute { get; set; }

		public int CurrentDistance { get; set; }

		[DisplayName("Riders")]
        public IEnumerable<TeamDetailsUserViewModel> Users { get; set; }
    }
}