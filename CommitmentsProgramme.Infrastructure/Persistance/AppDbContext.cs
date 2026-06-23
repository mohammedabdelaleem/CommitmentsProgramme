using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CommitmentsProgramme.Infrastructure.Persistance
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

    public DbSet<Commitment> Commitments { get; set; }
    public DbSet<CommitmentType> CommitmentType { get; set; }
    public DbSet<Rank> Ranks { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<DailyPlan> DailyPlans { get; set; }
    public DbSet<Officer> Officers { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Attendance> Attendances { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			// change on delete behaviour
			var cascadeFKs = builder.Model
				.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
				.Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade && !fk.IsOwnership);

			foreach (var relationship in cascadeFKs)
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}

			base.OnModelCreating(builder);
		}
	}
}


