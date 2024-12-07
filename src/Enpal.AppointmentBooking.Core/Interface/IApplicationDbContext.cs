namespace Enpal.AppointmentBooking.Core.Interface;

public interface IApplicationDbContext
{
    IQueryable<T> Set<T>()
        where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
