namespace Calendar.Abstraction.Interfaces
{
    public interface IWriteRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
