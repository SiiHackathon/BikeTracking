using BikeTracker.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BikeTracker.Data
{
	public class BikeTrackerDbContext : DbContext
	{
        public BikeTrackerDbContext() : base("BikeTrackerDb")
		{
            Database.SetInitializer<BikeTrackerDbContext>(null);
        }

		public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<News> News { get; set; }

	    protected override void OnModelCreating(DbModelBuilder modelBuilder)
	    {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<News>().Map(m => m.Requires("IsDeleted").HasValue(false))
                                       .Ignore(m => m.IsDeleted);
        }
	}
}