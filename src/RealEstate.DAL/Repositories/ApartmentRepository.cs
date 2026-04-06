using Microsoft.EntityFrameworkCore;
using RealEstate.BLL.Interfaces;
using RealEstate.DAL.Persistence;
using RealEstate.Domain.Entities;

namespace RealEstate.DAL.Repositories;

public sealed class ApartmentRepository : IApartmentRepository
{
    private readonly AppDbContext _dbContext;

    public ApartmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Apartment?> FindByAddressAndOwnerAsync(string address, int ownerId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Apartments.FirstOrDefaultAsync(
            x => x.Address == address && x.OwnerId == ownerId,
            cancellationToken);
    }

    public Task AddAsync(Apartment apartment, CancellationToken cancellationToken = default)
    {
        return _dbContext.Apartments.AddAsync(apartment, cancellationToken).AsTask();
    }

    public Task<bool> PhotoExistsAsync(int apartmentId, string url, string description, CancellationToken cancellationToken = default)
    {
        return _dbContext.Photos.AnyAsync(
            x => x.ApartmentId == apartmentId && x.Url == url && x.Description == description,
            cancellationToken);
    }

    public Task<bool> ReviewExistsAsync(int apartmentId, string reviewName, string reviewText, CancellationToken cancellationToken = default)
    {
        return _dbContext.Reviews.AnyAsync(
            x => x.ApartmentId == apartmentId && x.Name == reviewName && x.Text == reviewText,
            cancellationToken);
    }

    public Task<bool> RatingExistsAsync(int apartmentId, int score, CancellationToken cancellationToken = default)
    {
        return _dbContext.Ratings.AnyAsync(
            x => x.ApartmentId == apartmentId && x.Score == score,
            cancellationToken);
    }

    public Task<bool> ReservationExistsAsync(int apartmentId, DateTime startDate, DateTime endDate, string status, CancellationToken cancellationToken = default)
    {
        return _dbContext.Reservations.AnyAsync(
            x => x.ApartmentId == apartmentId
                && x.StartDate == startDate
                && x.EndDate == endDate
                && x.Status == status,
            cancellationToken);
    }

    public Task AddPhotoAsync(Photo photo, CancellationToken cancellationToken = default)
    {
        return _dbContext.Photos.AddAsync(photo, cancellationToken).AsTask();
    }

    public Task AddReviewAsync(Review review, CancellationToken cancellationToken = default)
    {
        return _dbContext.Reviews.AddAsync(review, cancellationToken).AsTask();
    }

    public Task AddRatingAsync(Rating rating, CancellationToken cancellationToken = default)
    {
        return _dbContext.Ratings.AddAsync(rating, cancellationToken).AsTask();
    }

    public Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken = default)
    {
        return _dbContext.Reservations.AddAsync(reservation, cancellationToken).AsTask();
    }
}
