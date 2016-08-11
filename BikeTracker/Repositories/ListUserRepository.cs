using System.Collections.Generic;
using System.Linq;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class ListUserRepository : IUserRepository
    {
        private static object locker = new object();
        private static ICollection<User> userCollection = new List<User>();

        public User GetById(long id)
        {
            return userCollection.FirstOrDefault(u => u.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return userCollection;
        }

        public long Save(User user)
        {
            lock (locker)
            {
                var existingUser = GetById(user?.UserId ?? 0);
                if (existingUser != null)
                {
                    userCollection.Remove(existingUser);
                    user.UserId = existingUser.UserId;
                }
                else if (userCollection.Any())
                {
                    user.UserId = userCollection.Max(u => u.UserId) + 1;
                }
                else
                {
                    user.UserId = 1;
                }
                userCollection.Add(user);
                return user.UserId;
            }
        }
    }
}