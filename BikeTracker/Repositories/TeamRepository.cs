using BikeTracker.Entities;

namespace BikeTracker.Repositories
{
    public class TeamRepository : EFRepository<Team>, ITeamRepository
    {
        public Team GetById(long id)
        {
            return GetBy(x => x.TeamId == id);
        }
        
        public bool DeleteById(long id)
        {
            return DeleteBy(x => x.TeamId == id);
        }
    }
}
