using System;

namespace BikeTracker.Entities
{
    public class News : Entity
    {
        public override long Id => NewsId;

        public long NewsId { get; set; }
        public DateTime AddedOn { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}