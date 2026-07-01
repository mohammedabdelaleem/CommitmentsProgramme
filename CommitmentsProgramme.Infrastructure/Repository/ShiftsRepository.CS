using CommitmentsProgramme.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class ShiftsRepository : GenericRepository<Shifts>,IShiftsRepository
{
  private readonly AppDbContext _context;

  public ShiftsRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

    

    // custome for this service 

}


