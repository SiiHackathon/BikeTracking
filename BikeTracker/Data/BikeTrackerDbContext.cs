﻿using BikeTracker.Entities;
using System.Data.Entity;

namespace BikeTracker.Data
{
	public class BikeTrackerDbContext : DbContext
	{
		public BikeTrackerDbContext() : base("BikeTrackerDb")
		{					  
		}

		public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Distance> Distances { get; set; }
	}
}