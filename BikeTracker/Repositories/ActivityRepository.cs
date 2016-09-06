using System.Collections.Generic;
using System.Linq;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class ActivityRepository : EFRepository<Activity>, IActivityRepository
    {
        public IEnumerable<Activity> GetByUserId(long userId)
        {
            return _dbContext.Activities.Where(x => x.UserId == userId).ToList();
        }

        public bool DeleteById(long id)
        {
            return DeleteBy(x => x.ActivityId == id);
        }
    }
}