
namespace CommitmentsProgramme.Infrastructure.Persistance.EntitiesConfigurations;

public class CommitmentsAttendancesConfigurations : IEntityTypeConfiguration<CommitmentsAttendances>
{
	public void Configure(EntityTypeBuilder<CommitmentsAttendances> builder)
	{
        builder
                .HasKey(x => new
                {
                    x.CommitmentId,
                    x.AttendanceId
                });

        builder.HasIndex(x =>
         new
         {
             x.CommitmentId,
             x.AttendanceId
         });

        builder.HasOne(x => x.Commitment)
         .WithMany(x => x.CommitmentsAttendances)
         .HasForeignKey(x => x.CommitmentId);

        builder.HasOne(x => x.Attendance)
            .WithMany(x => x.CommitmentsAttendances)
            .HasForeignKey(x => x.AttendanceId);
    }
}


