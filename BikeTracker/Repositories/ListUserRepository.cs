using System.Collections.Generic;
using System.Linq;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class ListUserRepository : IUserRepository
    {
        private static readonly object _locker = new object();
        private static readonly ICollection<User> _userCollection = new List<User>();

        public User GetById(long id)
        {
            return _userCollection.FirstOrDefault(u => u.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userCollection;
        }

        public void Save(User user)
        {
            lock (_locker)
            {
                var existingUser = GetById(user?.UserId ?? 0);
                if (existingUser != null)
                {
                    _userCollection.Remove(existingUser);
                    user.UserId = existingUser.UserId;
                }
                else if (_userCollection.Any())
                {
                    user.UserId = _userCollection.Max(u => u.UserId) + 1;
                }
                else
                {
                    user.UserId = 1;
                }
                _userCollection.Add(user);
            }
        }
    }
}