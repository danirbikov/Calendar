using Calendar.Abstraction;
using Calendar.Abstraction.Interfaces;
using Calendar.Data;
using Calendar.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Calendar.Repositories
{
    public class GenericRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;


        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #region IReadRepository
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.IsActive).ToListAsync();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.Where(x => x.IsActive);
        }
        #endregion

        #region IWriteRepository
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.DeletedAt = DateTime.UtcNow;
                entity.IsActive = false;
                await UpdateAsync(entity);
            }
            await _context.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        #endregion
    }
}
