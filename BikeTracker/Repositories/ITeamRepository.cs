using BikeTracker.Entities;
using System.Collections.Generic;

namespace BikeTracker.Repositories
{
    public interface ITeamRepository
    {
        Team GetById(long id);

        IEnumerable<Team> GetAll();

        void Save(Team team);
    }
}
