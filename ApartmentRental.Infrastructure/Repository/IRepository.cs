namespace ApartmentRental.Infrastructure.Repository;

public interface IRepository<T>
{
    Task<T> GetById(int id);
    
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteById(int id);
}