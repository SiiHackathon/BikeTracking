using System;

namespace BikeTracker.Entities
{
    public class Activity
    {
        public long ActivityId { get; set; }
        public long UserId { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal Distance { get; set; }
    }
}