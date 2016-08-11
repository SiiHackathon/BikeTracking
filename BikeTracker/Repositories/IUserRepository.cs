﻿using BikeTracker.Entities;
using System.Collections.Generic;

namespace BikeTracker.Repositories
{
    public interface IUserRepository
    {
        User GetById(long id);

        IEnumerable<User> GetAll();

        long Save(User user);
    }
}
