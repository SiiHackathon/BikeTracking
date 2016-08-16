namespace BikeTracker.Entities
{
    public class Team : Entity
    {
        public override long Id => TeamId;

        public long TeamId { get; set; }
        public string Name { get; set; }
        public bool ReverseRoute { get; set; }
        public int CurrentDistance { get; set; }
        public int TracksCompleted { get; set; }
        public string Image { get; set; }
    }
}