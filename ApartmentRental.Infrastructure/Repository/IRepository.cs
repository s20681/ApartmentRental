namespace ApartmentRental.Infrastructure.Repository;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteById(int id);
}