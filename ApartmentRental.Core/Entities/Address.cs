namespace ApartmentRental.Core.Entities;

public class Address : BaseEntity
{
    public string Ulica { get; set; }
    public string? NumerMieszkania { get; set; }
    public string? NumerBudynku { get; set; }
    public string Miasto { get; set; }
    public string KodPocztowy { get; set; }
    public string Kraj { get; set; }
}