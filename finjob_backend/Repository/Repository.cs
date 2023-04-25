using finjob_backend.Data;
using finjob_backend.Models;
using finjob_backend.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace finjob_backend.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }


            return await query.FirstOrDefaultAsync();
        }

        public async Task<PaginationResult<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? search = null, int pageSize = 0, int pageNumber = 1, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.ToString().Contains(search));
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            var totalCount = await query.CountAsync();

            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            var data = await query.ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var currentPage = pageNumber;

            return new PaginationResult<T>
            {
                Data = data,
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = currentPage
            };
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

    }
}
