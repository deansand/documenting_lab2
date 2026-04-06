namespace RealEstate.Domain.Entities;

public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int ApartmentId { get; set; }
    public Apartment Apartment { get; set; } = null!;
}
