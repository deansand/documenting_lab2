namespace RealEstate.Domain.Entities;

public class Rating
{
    public int Id { get; set; }
    public int Score { get; set; }

    public int ApartmentId { get; set; }
    public Apartment Apartment { get; set; } = null!;
}
