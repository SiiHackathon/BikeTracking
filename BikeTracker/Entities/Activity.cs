using System;

namespace BikeTracker.Entities
{
    public class Activity : Entity
    {
        public override long Id => ActivityId;

        public long ActivityId { get; set; }
        public long UserId { get; set; }
        public DateTime ActivityDate { get; set; }
        public int Distance { get; set; }
    }
}