using CommitmentsProgramme.Application.Interfaces;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
{
  private readonly AppDbContext _context;

  public AttendanceRepository(AppDbContext context) : base(context)
  {
	this._context = context;
  }

  // custome for this service 

}


