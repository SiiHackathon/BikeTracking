namespace BikeTracker.Entities
{
    public class Team
    {
        public long TeamId { get; set; }

        public string Name { get; set; }

        public bool ReverseRoute { get; set; }

        public int CurrentDistance { get; set; }

        public bool HasFinished { get; set; }

        public string Image { get; set; }
    }
}