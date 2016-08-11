using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public interface IDistanceRepository
    {
        long Save(Distance distance);
    }

    public class ListDistanceRepository : IDistanceRepository
    {
        public long Save(Distance distance)
        {
            throw new System.NotImplementedException();
        }
    }
}
