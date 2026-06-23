using CommitmentsProgramme.Application.DTOs;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
	private readonly AppDbContext context;


	public IDailyPlanRepository DailyPlans { get; set; }
	public IBranchRepository Branches { get; set; }
	public ICommitmentRepository Commitments { get; set; }
	public ICommitmentTypeRepository CommitmentTypes { get; set; }
	public IOfficerRepository Officers { get; set; }
	public IPriorityRepository Priorities { get; set; }
	public IRankRepository Ranks { get; set; }
	

    public UnitOfWork(AppDbContext context)
	{
		this.context = context;

		DailyPlans = new DailyPlanRepository(context);
		Branches = new BranchRepository(context);
		Commitments = new CommitmentRepository(context);
    CommitmentTypes = new CommitmentTypeRepository(context);
    Officers = new OfficerRepository(context);
    Priorities = new PriorityRepository(context);
    Ranks = new RankRepository(context);
		

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


