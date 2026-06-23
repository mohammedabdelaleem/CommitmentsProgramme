using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class BranchRepository : GenericRepository<Branch>, IBranchRepository
{
  private readonly AppDbContext _context;

  public BranchRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 

}


