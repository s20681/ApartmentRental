namespace ApartmentRental.Core.Entities;

public class Landlord : BaseEntity
{
    public Apartment[] Apartments { get; set; }

    public int AccountId { get; set; }
    public Account Account { get; set; }
}