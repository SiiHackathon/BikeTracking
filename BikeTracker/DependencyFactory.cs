using BikeTracker.Repositories;
using BikeTracker.Services;

namespace BikeTracker
{
    public static class DependencyFactory
    {
        public static IUserRepository CreateUserRepository => new ListUserRepository();
        public static ITeamRepository CreateTeamRepository => new ListTeamRepository();
        public static IActivityRepository CreateActivityRepository => new ListActivityRepository();

        public static IActivityService CreateActivityService => new ActivityService();
    }
}