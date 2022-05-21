namespace ApartmentRental.Core.DTO;

public class LandLordCreationRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Street { get; set; }
    public string ApartmentNumber { get; set; }
    public string BuildingNumber { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}