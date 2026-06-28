using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class TrafficOfficerRepository : GenericRepository<TrafficOfficer>,ITrafficOfficerRepository
{
  private readonly AppDbContext _context;

  public TrafficOfficerRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}

