
namespace CommitmentsProgramme.Application.Interfaces;


public interface IUnitOfWork : IDisposable
    {
    
		//IVillaRepository  Villa { get; set; }
		

    Task<int> CompleteAsync(CancellationToken cancellationToken = default); // save changes
    }



