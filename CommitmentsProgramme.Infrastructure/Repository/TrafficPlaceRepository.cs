
namespace CommitmentsProgramme.Infrastructure.Repository;

public class TrafficPlaceRepository : GenericRepository<TrafficPlace>,ITrafficPlaceRepository
{
  private readonly AppDbContext _context;

  public TrafficPlaceRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


