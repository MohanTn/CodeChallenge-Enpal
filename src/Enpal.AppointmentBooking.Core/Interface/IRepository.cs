namespace Enpal.AppointmentBooking.Core.Interface;

public interface IRepository<T>
    where T : class
{
    Task<T> GetByIdAsync(int Id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
