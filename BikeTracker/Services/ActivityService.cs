using System.Linq;

namespace BikeTracker.Services
{
    public class ActivityService : IActivityService
    {
        public decimal GetUserTotalDistance(long userId)
        {
            return DependencyFactory.CreateActivityRepository.GetByUserId(userId)
                .Sum(activity => (decimal)activity.Distance / 1000);
        }
    }
}