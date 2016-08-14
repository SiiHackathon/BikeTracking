using System;
using System.Collections.Generic;
using System.Linq;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class ListTeamRepository : ITeamRepository
    {
        private static object _locker = new object();
        private static ICollection<Team> _teamCollection = new List<Team>
        {
            new Team
            {
                TeamId = 1,
                Name = "WebMinions"
            }
        };

        public Team GetById(long id)
        {
            return _teamCollection.FirstOrDefault(u => u.TeamId == id);
        }

        public IEnumerable<Team> GetAll()
        {
            return _teamCollection;
        }

        public void Save(Team team)
        {
            lock (_locker)
            {
                var existingUser = GetById(team?.TeamId ?? 0);
                if (existingUser != null)
                {
                    _teamCollection.Remove(existingUser);
                    team.TeamId = existingUser.TeamId;
                }
                else if (_teamCollection.Any())
                {
                    team.TeamId = _teamCollection.Max(u => u.TeamId) + 1;
                }
                else
                {
                    team.TeamId = 1;
                }
                _teamCollection.Add(team);
            }
        }

        public void DeleteById(long teamId)
        {
            lock (_locker)
            {
                var existingTeam = GetById(teamId);
                if (existingTeam != null)
                {
                    _teamCollection.Remove(existingTeam);
                }
            }
        }
    }
}