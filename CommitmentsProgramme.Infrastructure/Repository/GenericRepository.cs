using Mapster;
using Microsoft.EntityFrameworkCore.Query;

namespace CommitmentsProgramme.Infrastructure.Repository;

public class GenericRepository<T> :IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext context;
    private DbSet<T> dbSet;
    public GenericRepository(AppDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }


    public virtual async Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        //string? include = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int? size = null,
        int? skip = null,
          Expression<Func<T, object>>? orderByExpression = null,
        SharedData.OrderBy order = SharedData.OrderBy.Ascending,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbSet.AsQueryable();

        // Filtering
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Includes (string-based)
        //if (!string.IsNullOrWhiteSpace(include))
        //{
        //	foreach (var entity in include.Split(",", StringSplitOptions.RemoveEmptyEntries))
        //	{
        //		query = query.Include(entity.Trim());
        //	}
        //}

        if (include != null)
        {
            query = include(query);
        }

        // Order
        if (orderByExpression != null)
        {
            query = order == SharedData.OrderBy.Ascending
                ? query.OrderBy(orderByExpression)
                : query.OrderByDescending(orderByExpression);
        }

        // Paging (skip + take)
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (size.HasValue)
        {
            query = query.Take(size.Value);
        }


        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<SimpleEntity>> GetAllInShortAsync(
    Expression<Func<T, bool>>? predicate = null,
    //string? include = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
    int? size = null,
    int? skip = null,
    CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbSet.AsQueryable();

        // Filtering
        if (predicate != null)
        {
            query = query.Where(predicate);
        }



        if (include != null)
        {
            query = include(query);
        }

        // Paging (skip + take)
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }

        if (size.HasValue)
        {
            query = query.Take(size.Value);
        }

        return await query.
            ProjectToType<SimpleEntity>()
            .ToListAsync(cancellationToken);
    }


    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null,
  //string? include = null,
  Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = dbSet.AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        //if (include != null && !string.IsNullOrWhiteSpace(include))
        //{
        //	foreach (var entity in include.Split(","))
        //	{
        //		query = query.Include(entity);
        //	}
        //}

        if (include != null)
        {
            query = include(query);
        }

        return await query.SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task SaveAsync(T entity, string username, CancellationToken cancellationToken = default)
    {
        if (entity.Id == 0) // so wee need to ensure T is inherited from BaseEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = username;
            await dbSet.AddAsync(entity, cancellationToken);
        }
        else
        {
            //// i already have the tracked entity
            var entityDb = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
            if (entityDb is null)
                throw new InvalidOperationException($"Entity with Id {entity.Id} not found.");
            entity.Adapt(entityDb);

            entityDb.UpdatedDate = DateTime.UtcNow;
            entityDb.UpdatedBy = username;
        }
    }




    public virtual bool Remove(T entity)
    {

        dbSet.Remove(entity);
        return true;
    }

    public virtual bool RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
        return true;
    }
    public Task<bool> IsExistsAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        if (predicate is null)
            throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

        return dbSet.AnyAsync(predicate, cancellationToken);
    }



    public virtual async Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>? distinctByExpression = null,
        CancellationToken cancellationToken = default)
    {

        IQueryable<T> query = dbSet.AsQueryable();

        // Filtering
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (distinctByExpression is not null)
        {
            return await query
                .Select(distinctByExpression)
                .Distinct()
                .CountAsync(cancellationToken);
        }

        return await query.CountAsync(cancellationToken);
    }

    public IQueryable<T> GetQueryable(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = dbSet.AsQueryable();

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        if (include != null)
        {
            query = include(query);
        }

        return query;
    }

}


