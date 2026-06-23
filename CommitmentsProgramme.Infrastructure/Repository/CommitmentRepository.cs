using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class CommitmentRepository : GenericRepository<Commitment>, ICommitmentRepository
{
  private readonly AppDbContext _context;

  public CommitmentRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


