using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CommitmentsProgramme.Application.Interfaces;


public interface IGenericRepository<T> where T : class
{

		// context.Categories.ToList();
		// context.Categories.Include("Products").ToList();
		// context.Categories.Where(c=>c.Id==id).ToList();
		Task<List<T>> GetAllAsync(
				Expression<Func<T, bool>>? predicate = null,
				//string? include = null,
				Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
				int? size = null,
				int? skip = null,
				CancellationToken cancellationToken = default);


		Task<IEnumerable<SimpleEntity>> GetAllInShortAsync(
		Expression<Func<T, bool>>? predicate = null,
		Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
		int? size = null,
		int? skip = null,
		CancellationToken cancellationToken = default);


		// context.Categories.ToSingleOrDefault();
		// context.Categories.Include("Products").ToSingleOrDefault();
		// context.Categories.Where(c=>c.Id==id).ToSingleOrDefault();
		Task<T?> GetAsync(Expression<Func<T, bool>>? predicate = null,
				//string? include = null,
				Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
				 CancellationToken cancellationToken = default);


	Task<bool> IsExistsAsync(Expression<Func<T, bool>>? predicate = null,
								 CancellationToken cancellationToken = default);


	Task SaveAsync(T entity,string username, CancellationToken cancellationToken = default);


	bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);

    }


