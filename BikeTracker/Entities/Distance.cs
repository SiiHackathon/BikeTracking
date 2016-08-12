using System;

namespace BikeTracker.Entities
{
    public class Distance
    {
        public long UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Length { get; set; }
    }
}