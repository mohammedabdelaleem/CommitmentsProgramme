using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class RankRepository : GenericRepository<Rank>, IRankRepository
{
  private readonly AppDbContext _context;

  public RankRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


