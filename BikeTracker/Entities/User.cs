namespace BikeTracker.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long TeamId { get; set; }
        public string Image { get; set; }
    }
}