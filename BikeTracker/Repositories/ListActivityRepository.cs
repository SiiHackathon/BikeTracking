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
                        Distance = 9.5M
                    },
                    new Activity
                    {
                        ActivityId = userId << 1 + 1,
                        UserId = userId,
                        ActivityDate = new DateTime(2016, 8, 13),
                        Distance = 25.5M
                    }
                };
        }
    }
}