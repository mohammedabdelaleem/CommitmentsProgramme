
namespace CommitmentsProgramme.Infrastructure.Persistance.EntitiesConfigurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.HasData(new ApplicationUser
		{
			Id = DefaultUsers.AdminId,
			FullName = DefaultUsers.AdminName,
			Email = DefaultUsers.AdminEmail,
			NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
			UserName = DefaultUsers.AdminEmail,
			NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
			ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
			SecurityStamp = DefaultUsers.AdminSecurityStamp,
			EmailConfirmed = true, // don't forget this , We don't need default admin to sign in
			PasswordHash = DefaultUsers.AdminPasswordHash,
			PhoneNumber =DefaultUsers.AdminPhoneNumber ,
			CreatedAt = new DateTime(2026,12,10)
		},
		new ApplicationUser
		{
			Id = DefaultUsers.UserId,
			FullName = DefaultUsers.UserName,
			Email = DefaultUsers.UserEmail,
			NormalizedEmail = DefaultUsers.UserEmail.ToUpper(),
			UserName = DefaultUsers.UserEmail,
			NormalizedUserName = DefaultUsers.UserEmail.ToUpper(),
			ConcurrencyStamp = DefaultUsers.UserConcurrencyStamp,
			SecurityStamp = DefaultUsers.UserSecurityStamp,
			EmailConfirmed = true, 
			PasswordHash = DefaultUsers.UserPasswordHash,
			PhoneNumber = DefaultUsers.UserPhoneNumber,
			CreatedAt = new DateTime(2026, 12, 10)
		}

		

		);

	}
}


