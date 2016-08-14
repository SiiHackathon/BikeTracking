using BikeTracker.Entities;
using System.Collections.Generic;
using System;

namespace BikeTracker.Repositories
{
    public class ListActivityRepository : IActivityRepository
    {
        public IEnumerable<Activity> GetByUserId(long userId)
        {
            return new[]
                {
                    new Activity
                    {
                        ActivityId = userId << 1,
                        UserId = userId,
                        ActivityDate = new DateTime(2016, 8, 12),
                        Distance = 9500
                    },
                    new Activity
                    {
                        ActivityId = userId << 1 + 1,
                        UserId = userId,
                        ActivityDate = new DateTime(2016, 8, 13),
                        Distance = 25500
                    }
                };
        }

        public void Save(Activity activity)
        {
            // TODO
        }

        public bool DeleteById(long id)
        {
            return false;
        }
    }

    
}