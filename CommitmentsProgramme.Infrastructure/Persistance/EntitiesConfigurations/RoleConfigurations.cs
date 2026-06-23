
namespace CommitmentsProgramme.Infrastructure.Persistance.EntitiesConfigurations;

public class RoleConfigurations : IEntityTypeConfiguration<ApplicationRole>
{
	public void Configure(EntityTypeBuilder<ApplicationRole> builder)
	{

		builder.HasData(
		[
		new ApplicationRole {
				Id = DefaultRoles.AdminRoleId,
				Name = DefaultRoles.Admin,
				DisplayName = DefaultRoles.AdminDisplayName,
				NormalizedName = DefaultRoles.Admin.ToUpper(),
				ConcurrencyStamp = DefaultRoles.AdminConcurrencyStamp,CreatedAt = new DateTime(2026,12,10)
			},
			new ApplicationRole {
				Id = DefaultRoles.MemberRoleId,
				Name = DefaultRoles.Member,
                DisplayName = DefaultRoles.MemberDisplayName,
                NormalizedName = DefaultRoles.Member.ToUpper(),
				ConcurrencyStamp = DefaultRoles.MemberConcurrencyStamp,
				IsDefault = true,CreatedAt = new DateTime(2026,12,10)
			}
		]
		);

	}


}


