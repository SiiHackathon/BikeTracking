using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BikeTracker.Data;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class EFTeamRepository : ITeamRepository
    {
        private readonly BikeTrackerDbContext _dbContext = new BikeTrackerDbContext();

        public Team GetById(long id)
        {
            return _dbContext.Teams.SingleOrDefault(x => x.TeamId == id);
        }

        public IEnumerable<Team> GetAll()
        {
            return _dbContext.Teams.ToList();
        }

        public void Save(Team team)
        {
            if (_dbContext.Entry(team).State == EntityState.Detached)
                _dbContext.Teams.Add(team);

            _dbContext.SaveChanges();
        }
        
        public bool DeleteById(long id)
        {
            var team = _dbContext.Teams.SingleOrDefault(x => x.TeamId == id);
            if (team == null)
                return false;
            _dbContext.Teams.Remove(team);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
