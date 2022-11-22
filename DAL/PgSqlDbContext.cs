using System;
using System.Reflection.Metadata;
using CodeChallenge.DAL;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.DAL
{
	public class PgSqlDbContext : DbContext
	{
		public PgSqlDbContext(DbContextOptions<PgSqlDbContext> options)
			:base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Weather>()
				.HasIndex(x => x.Latitude);

			modelBuilder.Entity<Weather>()
				.HasIndex(x => x.Longitude);

			modelBuilder.Entity<Weather>()
				.HasData(new List<Weather>
				{
					new Weather(0,0,"sunny")
				});
        }

        public DbSet<Weather> Weathers{ get; set; }

	}
}

