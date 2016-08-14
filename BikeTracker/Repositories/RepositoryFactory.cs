namespace BikeTracker.Repositories
{
    public static class RepositoryFactory
    {
        public static IUserRepository CreateUserRepository => new ListUserRepository();
        public static ITeamRepository CreateTeamRepository => new ListTeamRepository();
        public static IDistanceRepository CreateDistanceRepository => new ListDistanceRepository();
    }
}