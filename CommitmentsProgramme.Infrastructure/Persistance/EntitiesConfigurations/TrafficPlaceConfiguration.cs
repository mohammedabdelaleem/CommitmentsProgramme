namespace CommitmentsProgramme.Infrastructure.Persistance.EntitiesConfigurations;

public class TrafficPlaceConfiguration : IEntityTypeConfiguration<TrafficPlace>
{
	public void Configure(EntityTypeBuilder<TrafficPlace> builder)
    {
        



			 builder
        .HasKey(tp => new { tp.TrafficId, tp.PlaceId });



    builder
        .HasOne(tp => tp.place)
        .WithMany(t => t.TrafficPlaces)
        .HasForeignKey(tp => tp.TrafficId);

    builder
        .HasOne(tp => tp.place)
        .WithMany(p => p.TrafficPlaces)
        .HasForeignKey(tp => tp.PlaceId);




    }

}