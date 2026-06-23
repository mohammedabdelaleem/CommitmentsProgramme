using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class DailyPlanRepository : GenericRepository<DailyPlan>, IDailyPlanRepository
{
  private readonly AppDbContext _context;

  public DailyPlanRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 



}


