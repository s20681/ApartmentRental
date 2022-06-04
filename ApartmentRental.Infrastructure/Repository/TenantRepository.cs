using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class TenantRepository : ITenantRepository
{
    private readonly MainContext _mainContext;

    public TenantRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Tenant>> GetAllAsync()
    {
        var tenants = await _mainContext.Tenant.ToListAsync();

        foreach (var tenant in tenants)
        {
            await _mainContext.Entry(tenant).Reference(x => x.Apartment).LoadAsync();
        }

        return tenants;
    }

    public async Task<Tenant> GetById(int id)
    {
        var tenant = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
        if (tenant != null)
        {
            await _mainContext.Entry(tenant).Reference(x => x.Apartment).LoadAsync();
            return tenant;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Tenant entity)
    {
        var tenant = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Apartment == entity.Apartment);

        if (tenant != null)
        {
            throw new EntityAlreadyExistException();
        }

        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tenant entity)
    {
        var tenantToUpdate = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (tenantToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        tenantToUpdate.Account = entity.Account;
        tenantToUpdate.Apartment = entity.Apartment;
        tenantToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var tenantToDelete = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
        if (tenantToDelete != null)
        {
            _mainContext.Tenant.Remove(tenantToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            throw new EntityNotFoundException();
        }
    }
}