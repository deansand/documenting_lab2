namespace RealEstate.Domain.Entities;

public class Apartment
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int RoomCount { get; set; }

    public int OwnerId { get; set; }
    public Owner Owner { get; set; } = null!;

    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
