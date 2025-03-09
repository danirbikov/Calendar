using System.Linq.Expressions;

namespace Calendar.Abstraction.Interfaces
{
    public interface IReadRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> AsQueryable();
    }
}
