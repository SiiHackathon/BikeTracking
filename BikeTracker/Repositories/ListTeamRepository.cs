using System.Collections.Generic;
using System.Linq;
using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class ListTeamRepository : ITeamRepository
    {
        private static object locker = new object();
        private static ICollection<Team> teamCollection = new List<Team>
        {
            new Team
            {
                TeamId = 1,
                Name = "WebMinions"
            }
        };

        public Team GetById(long id)
        {
            return teamCollection.FirstOrDefault(u => u.TeamId == id);
        }

        public IEnumerable<Team> GetAll()
        {
            return teamCollection;
        }

        public void Save(Team team)
        {
            lock (locker)
            {
                var existingUser = GetById(team?.TeamId ?? 0);
                if (existingUser != null)
                {
                    teamCollection.Remove(existingUser);
                    team.TeamId = existingUser.TeamId;
                }
                else if (teamCollection.Any())
                {
                    team.TeamId = teamCollection.Max(u => u.TeamId) + 1;
                }
                else
                {
                    team.TeamId = 1;
                }
                teamCollection.Add(team);
            }
        }
    }
}