
namespace CommitmentsProgramme.Application.Interfaces;


public interface IUnitOfWork : IDisposable
    {

  IDailyPlanRepository DailyPlans { get; set; }
  IBranchRepository Branches { get; set; }
  ICommitmentRepository Commitments { get; set; }
  ICommitmentTypeRepository CommitmentTypes { get; set; }
  IOfficerRepository Officers { get; set; }
  IPriorityRepository Priorities { get; set; }
  IRankRepository Ranks { get; set; }


  Task<int> CompleteAsync(CancellationToken cancellationToken = default); // save changes
    }



