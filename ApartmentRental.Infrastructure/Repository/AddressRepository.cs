using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly MainContext _mainContext;

    public AddressRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        var addresses = await _mainContext.Address.ToListAsync();
        return addresses;
    }

    public async Task<Address> GetById(int id)
    {
        var address = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        if (address != null)
        {
            return address;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Address entity)
    {
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Address entity)
    {
        var addressToUpdate = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (addressToUpdate == null)
        {
            throw new EntityNotFoundException();
        }

        addressToUpdate.City = entity.City;
        addressToUpdate.Country = entity.Country;
        addressToUpdate.Street = entity.Street;
        addressToUpdate.ApartmentNumber = entity.ApartmentNumber;
        addressToUpdate.BuildingNumber = entity.BuildingNumber;
        addressToUpdate.ZipCode = entity.ZipCode;
        addressToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var addressToDelete = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        if (addressToDelete != null)
        {
            _mainContext.Address.Remove(addressToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            throw new EntityNotFoundException();
        }
    }

    public async Task<int> GetAddressIdByItsAttributesAsync(string country, string city, string zipCode, string street, string buildingNumber,
        string apartmentNumber)
    {
        var address = await _mainContext.Address.FirstOrDefaultAsync(x =>
            x.Country == country && x.City == city && x.ZipCode == zipCode && x.Street == street
            && x.BuildingNumber == buildingNumber && x.ApartmentNumber == apartmentNumber);
        return address?.Id ?? 0;
    }

    public async Task<Address> CreateAndGetAsync(Address address)
    {
        address.DateOfCreation = DateTime.UtcNow;
        address.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.AddAsync(address);
        await _mainContext.SaveChangesAsync();

        return address;
    }
}