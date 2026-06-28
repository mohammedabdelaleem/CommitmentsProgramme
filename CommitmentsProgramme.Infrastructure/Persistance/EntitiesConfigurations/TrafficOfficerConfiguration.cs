namespace CommitmentsProgramme.Infrastructure.Persistance.EntitiesConfigurations;

public class TrafficOfficerConfiguration : IEntityTypeConfiguration<TrafficOfficer>
{
	public void Configure(EntityTypeBuilder<TrafficOfficer> builder)
    {
        

builder
    .HasKey(to => new { to.TrafficPlaneId, to.OfficerId });

builder
    .HasOne(to => to.trafficPlane)
    .WithMany(tp => tp.TrafficOfficer)
    .HasForeignKey(to => to.TrafficPlaneId);

builder
    .HasOne(to => to.officer)
    .WithMany(o => o.TrafficOfficer)
    .HasForeignKey(to => to.OfficerId);


    }

}