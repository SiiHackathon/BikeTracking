using BikeTracker.Repositories;
using BikeTracker.Services;

namespace BikeTracker
{
    public static class DependencyFactory
    {
        public static IUserRepository CreateUserRepository => new UserRepository();
        public static ITeamRepository CreateTeamRepository => new TeamRepository();
        public static IActivityRepository CreateActivityRepository => new ActivityRepository();

        public static IActivityService CreateActivityService => new ActivityService();
    }
}