using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class CommitmentTypeRepository : GenericRepository<CommitmentType>, ICommitmentTypeRepository
{
  private readonly AppDbContext _context;

  public CommitmentTypeRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


