namespace RealEstate.BLL.Contracts;

public sealed class ImportResult
{
    public int RowsRead { get; set; }
    public int OwnersCreated { get; set; }
    public int ApartmentsCreated { get; set; }
    public int PhotosCreated { get; set; }
    public int ReviewsCreated { get; set; }
    public int RatingsCreated { get; set; }
    public int ReservationsCreated { get; set; }
}
