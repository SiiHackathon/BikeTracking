using System.Collections.Generic;
using System.Linq;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class UserRepository :EFRepository<User>, IUserRepository
    {
        public User GetById(long id)
        {
            return GetBy(x => x.UserId == id);
        }

        public IEnumerable<User> GetByTeamId(long teamId)
        {
            return _dbContext.Users.Where(x => x.TeamId == teamId).ToList();
        }

        public bool DeleteById(long id)
        {
            return DeleteBy(x => x.UserId == id);
        }
    }
}