using System.Diagnostics.CodeAnalysis;

namespace ApartmentRental.Infrastructure.Entities;

public class Address : BaseEntity
{
    public string Street { get; set; }
    public string? ApartmentNumber { get; set; }
    public string? BuildingNumber { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}