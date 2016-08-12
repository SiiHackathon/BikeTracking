using System;

namespace BikeTracker.Models
{
    public class TeamStandingsModel
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        public int CurrentDistance { get; set; }
    }
}