using System.Linq.Expressions;

namespace CommitmentsProgramme.Infrastructure.Services;

public interface IUserService
{
    Task<ApplicationUser?> GetAsync(
        Expression<Func<ApplicationUser, bool>> predicate,
        Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>>? include = null,
        bool tracked = false,
        CancellationToken cancellationToken = default);

    Task<List<ApplicationUser>> GetAllAsync(
        Expression<Func<ApplicationUser, bool>>? predicate = null,
        Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>>? include = null,
        CancellationToken cancellationToken = default);

}