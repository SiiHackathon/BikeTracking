using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public interface IUserRepository
    {
        User GetById(long id);

        long Save(User user);
    }
}
