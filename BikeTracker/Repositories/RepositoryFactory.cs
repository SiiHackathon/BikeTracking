namespace BikeTracker.Repositories
{
    public static class RepositoryFactory
    {
        public static IUserRepository CreateUserRepository => new ListUserRepository();
    }
}