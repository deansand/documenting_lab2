namespace RealEstate.BLL.Contracts;

public sealed class ImportCsvRecord
{
    public string OwnerName { get; set; } = string.Empty;
    public string OwnerContactInfo { get; set; } = string.Empty;
    public string ApartmentAddress { get; set; } = string.Empty;
    public decimal ApartmentPrice { get; set; }
    public int ApartmentRoomCount { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    public string PhotoDescription { get; set; } = string.Empty;
    public string ReviewName { get; set; } = string.Empty;
    public string ReviewText { get; set; } = string.Empty;
    public int? RatingScore { get; set; }
    public DateTime? ReservationStartDate { get; set; }
    public DateTime? ReservationEndDate { get; set; }
    public string ReservationStatus { get; set; } = string.Empty;
}
