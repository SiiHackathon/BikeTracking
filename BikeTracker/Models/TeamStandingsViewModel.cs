using System;

namespace BikeTracker.Models
{
    public class TeamStandingsViewModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal CurrentDistance { get; set; }

        public int FinishedLaps { get; set; }

		public decimal DistanceToGo { get; set; }
		
    }
}