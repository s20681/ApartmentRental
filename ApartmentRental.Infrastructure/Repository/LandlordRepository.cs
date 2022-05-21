using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class LandlordRepository : ILandlordRepository
{
    private readonly MainContext _mainContext;

    public LandlordRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Landlord>> GetAll()
    {
        var landlords = await _mainContext.Landlord.ToListAsync();

        foreach (var landlord in landlords)
        {
            await _mainContext.Entry(landlord).Reference(x => x.Apartments).LoadAsync();
        }

        return landlords;
    }

    public async Task<Landlord> GetById(int id)
    {
        var landlord = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
        if (landlord != null)
        {
            await _mainContext.Entry(landlord).Reference(x => x.Apartments).LoadAsync();
            return landlord;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Landlord entity)
    {
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Landlord entity)
    {
        var landlordToUpdate = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (landlordToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        landlordToUpdate.Account = entity.Account;
        landlordToUpdate.Apartments = entity.Apartments;
        landlordToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var landlordToDelete = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
        if (landlordToDelete != null)
        {
            _mainContext.Landlord.Remove(landlordToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            throw new EntityNotFoundException();
        }
    }
}