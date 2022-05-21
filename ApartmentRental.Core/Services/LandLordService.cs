using ApartmentRental.Core.DTO;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Repository;

namespace ApartmentRental.Core.Services;

public class LandLordService : ILandLordService
{
    private readonly ILandlordRepository _landlordRepository;
    private readonly IAddressService _addressService;
    private readonly IAccountRepository _accountRepository;

    public LandLordService(ILandlordRepository landlordRepository, IAddressService addressService,
        IAccountRepository accountRepository)
    {
        _landlordRepository = landlordRepository;
        _addressService = addressService;
        _accountRepository = accountRepository;
    }

    public async Task AddNewLandLord(LandLordCreationRequestDto dto)
    {
        var addressId = await _addressService.GetAddressIdOrCreateAsync(dto.Country,
            dto.City, dto.ZipCode, dto.Street, dto.BuildingNumber, dto.ApartmentNumber);

        var account = await _accountRepository.CreateAndGetAsync(new Account
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            IsAccountActive = true,
            AddressId = addressId
        });

        await _landlordRepository.AddAsync(new Landlord
        {
            AccountId = account.Id
        });
    }
}