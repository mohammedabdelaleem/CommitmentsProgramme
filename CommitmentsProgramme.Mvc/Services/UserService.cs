using System.Linq;
using System.Linq.Expressions;

namespace CommitmentsProgramme.Mvc.Services;

public class UserService(AppDbContext context) : IUserService
{
    private readonly AppDbContext _context = context;

   
    public async Task<ApplicationUser?> GetAsync(
        Expression<Func<ApplicationUser, bool>> predicate,
        Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>>? include = null,
        bool tracked = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ApplicationUser> query = _context.Users;

        if (!tracked)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<ApplicationUser>> GetAllAsync(
        Expression<Func<ApplicationUser, bool>>? predicate = null,
        Func<IQueryable<ApplicationUser>, IQueryable<ApplicationUser>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<ApplicationUser> query = _context.Users.AsNoTracking();

        if (include != null)
            query = include(query);

        if (predicate != null)
            query = query.Where(predicate);

        return await query.ToListAsync(cancellationToken);
    }
}
