using ApartmentRental.Infrastructure.Entities;

namespace ApartmentRental.Infrastructure.Repository;

public interface IAddressRepository : IRepository<Address>
{
    Task<int> GetAddressIdByItsAttributesAsync(string country, string city, string zipCode, string street,
        string buildingNumber, string apartmentNumber);

    Task<Address> CreateAndGetAsync(Address address);
}