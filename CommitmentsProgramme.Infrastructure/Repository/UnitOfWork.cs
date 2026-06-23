namespace CommitmentsProgramme.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext context;


	//public IVillaRepository Villa { get; set; }
	

    public UnitOfWork(AppDbContext context)
	{
		this.context = context;

		//Villa = new VillaRepository(context);
		

    }
    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
	{
		return await context.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		context.Dispose();
	}


}


