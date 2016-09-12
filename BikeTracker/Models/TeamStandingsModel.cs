using System.Collections.Generic;

namespace BikeTracker.Models
{
    public class TeamStandingsModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int CurrentDistance { get; set; }
	}
}