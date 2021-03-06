﻿using BikeTracker.Entities;
using System.Collections.Generic;

namespace BikeTracker.Repositories
{
    public interface IUserRepository
    {
        User GetById(long id);
        IEnumerable<User> GetByTeamId(long teamId);
        IEnumerable<User> GetAll();
        void Save(User user);
        bool DeleteById(long id);
    }
}
