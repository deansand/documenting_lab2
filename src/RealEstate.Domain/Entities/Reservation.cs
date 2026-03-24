namespace RealEstate.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public int ApartmentId { get; set; }
    public Apartment Apartment { get; set; } = null!;
}
