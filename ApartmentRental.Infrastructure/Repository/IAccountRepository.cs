using ApartmentRental.Infrastructure.Entities;

namespace ApartmentRental.Infrastructure.Repository;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account> CreateAndGetAsync(Account account);
}