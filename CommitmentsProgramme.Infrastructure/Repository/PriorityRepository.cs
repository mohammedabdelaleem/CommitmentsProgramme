using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
{
  private readonly AppDbContext _context;

  public PriorityRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


