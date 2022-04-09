namespace ApartmentRental.Core.Entities;

public class Tenant : BaseEntity
{
    public Apartment MyApartment { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; }
    
    
}