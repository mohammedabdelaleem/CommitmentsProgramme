
namespace CommitmentsProgramme.Infrastructure.Persistance.EntitiesConfigurations;

public class CommitmentBranchConfigurations : IEntityTypeConfiguration<CommitmentBranch>
{
	public void Configure(EntityTypeBuilder<CommitmentBranch> builder)
	{
        builder
                .HasKey(x => new
                {
                    x.CommitmentId,
                    x.BranchId
                });

        builder.HasIndex(x=> 
         new
         {
             x.CommitmentId,
             x.BranchId
         });

      

        builder.HasOne(x => x.Commitment)
            .WithMany(x => x.CommitmentBranches)
            .HasForeignKey(x => x.CommitmentId);

        builder.HasOne(x => x.Branch)
            .WithMany(x => x.CommitmentBranches)
            .HasForeignKey(x => x.BranchId);
    }
}


