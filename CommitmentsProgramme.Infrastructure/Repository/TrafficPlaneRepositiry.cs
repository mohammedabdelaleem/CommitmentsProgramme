using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class TrafficPlaneRepositiry : GenericRepository<TrafficPlane>, ITrafficPlaneRepositiry
{
  private readonly AppDbContext _context;

  public TrafficPlaneRepositiry(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


