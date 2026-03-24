using RealEstate.Domain.Entities;

namespace RealEstate.BLL.Interfaces;

public interface IApartmentRepository
{
    Task<Apartment?> FindByAddressAndOwnerAsync(string address, int ownerId, CancellationToken cancellationToken = default);
    Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default);

    Task<bool> PhotoExistsAsync(int apartmentId, string url, string description, CancellationToken cancellationToken = default);
    Task<bool> ReviewExistsAsync(int apartmentId, string reviewName, string reviewText, CancellationToken cancellationToken = default);
    Task<bool> RatingExistsAsync(int apartmentId, int score, CancellationToken cancellationToken = default);
    Task<bool> ReservationExistsAsync(int apartmentId, DateTime startDate, DateTime endDate, string status, CancellationToken cancellationToken = default);

    Task AddPhotoAsync(Photo photo, CancellationToken cancellationToken = default);
    Task AddReviewAsync(Review review, CancellationToken cancellationToken = default);
    Task AddRatingAsync(Rating rating, CancellationToken cancellationToken = default);
    Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken = default);
}
