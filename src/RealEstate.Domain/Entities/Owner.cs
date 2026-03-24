namespace RealEstate.Domain.Entities;

public class Owner
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;

    public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}
