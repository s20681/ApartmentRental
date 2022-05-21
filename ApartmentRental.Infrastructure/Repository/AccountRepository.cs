using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly MainContext _mainContext;

    public AccountRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        var accounts = await _mainContext.Accounts.ToListAsync();
        return accounts;
    }

    public async Task<Account> GetById(int id)
    {
        var account = await _mainContext.Accounts.SingleOrDefaultAsync(x => x.Id == id);
        if (account != null)
        {
            return account;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Account entity)
    {
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Account entity)
    {
        var accountToUpdate = await _mainContext.Accounts.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (accountToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        accountToUpdate.FirstName = entity.FirstName;
        accountToUpdate.LastName = entity.LastName;
        accountToUpdate.Email = entity.Email;
        accountToUpdate.PhoneNumber = entity.PhoneNumber;
        accountToUpdate.IsAccountActive = entity.IsAccountActive;
        accountToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var accountToDelete = await _mainContext.Accounts.SingleOrDefaultAsync(x => x.Id == id);
        if (accountToDelete != null)
        {
            _mainContext.Accounts.Remove(accountToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            throw new EntityNotFoundException();
        }
    }

    public async Task<Account> CreateAndGetAsync(Account account)
    {
        account.DateOfCreation = DateTime.UtcNow;
        account.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.AddAsync(account);
        await _mainContext.SaveChangesAsync();

        return account;
    }

}