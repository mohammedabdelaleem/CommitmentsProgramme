using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class OfficerRepository : GenericRepository<Officer>, IOfficerRepository
{
  private readonly AppDbContext _context;

  public OfficerRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


