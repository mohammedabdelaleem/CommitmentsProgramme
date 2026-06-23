using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
{
  private readonly AppDbContext _context;

  public PlaceRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 

}


