using BikeTracker.Entities;
using System.Collections.Generic;

namespace BikeTracker.Repositories
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetByUserId(long userId);
        void Save(Activity activity);
        bool DeleteById(long id);
    }
}
