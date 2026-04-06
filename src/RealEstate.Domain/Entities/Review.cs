namespace RealEstate.Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

    public int ApartmentId { get; set; }
    public Apartment Apartment { get; set; } = null!;
}
